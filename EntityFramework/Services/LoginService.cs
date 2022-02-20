using EntityFramework.Interface;
using EntityFramework.Models;
using EntityFramework.Models.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EntityFramework.Services
{
    public class LoginService : ILoginService
    {
        private readonly string key;
        private readonly IBankRepository _repository;
        private static RNGCryptoServiceProvider? _CryptoService = new RNGCryptoServiceProvider();

        public LoginService(IBankRepository repository, IConfiguration config)
        {
            _repository = repository;
            key = config.GetSection("JWTkey").ToString();
        }

        public async Task<AuthenticateResponse> Authenticate(Users user, string ipaddress)
        {
            var response = await _repository.Login(user.Username, user.Password);
            

            if (response == null)
                return null;

            var jwtToken = generateJwtToken(response);
            var refreshToken = generateRefreshToken(ipaddress);
            response.RefreshToken?.Add(refreshToken);


            await _repository.UpdateSingleUser(response);

            return new AuthenticateResponse(response, jwtToken, refreshToken.Token);
        }


        public async Task<AuthenticateResponse> RefreshToken(string token, string ipaddress)
        {
            var user = await _repository.TokenRefreshRevoke(token);

            if (user == null)
                return null;


            var refreshToken = user.RefreshToken.SingleOrDefault(x => x.Token == token);
            if (!refreshToken.IsActive)
                return null;

            var newRefreshToken = generateRefreshToken(ipaddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipaddress;
            refreshToken.ReplaceByToken = newRefreshToken.Token;

            user.RefreshToken.Add(refreshToken);

            await _repository.UpdateSingleUser(user);

            var jwtToken = generateJwtToken(user);
            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipadress)
        {
            var user = await _repository.TokenRefreshRevoke(token);

            if (user == null)
                return false;

            var refreshToken = user.RefreshToken?.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
                return false;

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipadress;

            await _repository.UpdateSingleUser(user);

            return true;
        }

        private string generateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtkey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserID.ToString()),
                    //new Claim(ClaimTypes., user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var CryptoService = _CryptoService)
            {
                var randomBytes = new byte[64];
                _CryptoService.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        public async Task<Users> GetByID(string id)
        {
            return await _repository.GetAllUserData(id);
        }

    }
}
