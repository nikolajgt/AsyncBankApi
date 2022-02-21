using EntityFramework.Interface;
using EntityFramework.Models;
using EntityFramework.Repository;
using EntityFramework.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// MY stuff
var MyAllowSpecificOrigins = "AllowOrigin";
var config = builder.Configuration;

builder.Services.AddControllers();
//builder.Services.AddDbContext<MyContext>(opt => opt
//                .UseLazyLoadingProxies()
//                .UseMySql(config.GetConnectionString("mysql"), new MySqlServerVersion(new Version())), ServiceLifetime.Transient);

builder.Services.AddDbContext<MyContext>(opt => opt
                .UseLazyLoadingProxies()
                .UseSqlServer(config.GetConnectionString("SqlServer")), ServiceLifetime.Transient);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin", c =>
    {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.WithOrigins("http://localhost:4000");
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer( x =>
     {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("JWTkey").ToString())),
             ValidateIssuer = false,
             ValidateAudience = false
         };
     });



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
