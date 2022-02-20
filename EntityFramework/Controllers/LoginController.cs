using EntityFramework.Interface;
using EntityFramework.Models;
using EntityFramework.Models.DTO;
using EntityFramework.Models.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController
    {

        private IHttpContextAccessor _contextAccessor;
        private ILoginService _service;

        public LoginController(IHttpContextAccessor contextAccessor, ILoginService service)
        {
            _service = service;
            _contextAccessor = contextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> LoginForUser([FromBody] UserLoginDTO user)
        {
            var newuser = new Users(user.UserName, user.Password);

            var response = await _service.Authenticate(newuser, ipAddress());
            if (response == null)
                return new UnauthorizedObjectResult(response);

            return new OkObjectResult(response);
        }


        [HttpPost("Refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RevokeTokenRequest model)
        {
            var response = await _service.RefreshToken(model.Token, ipAddress());

            if (response == null)
                return new UnauthorizedObjectResult(new { message = "Invalid token" });

            return new OkObjectResult(response);
        }


        [HttpPost("Revoke-token")]
        public async Task<ActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            var response = await _service.RevokeToken(model.Token, ipAddress());
            if (response == null)
                return new NotFoundObjectResult(new { message = "Token not found" });

            return new OkObjectResult(response);
        }

        [HttpGet("{id}/test")]
        public IActionResult GetById(string id)
        {
            var user = _service.GetByID(id);
            if (user == null)
                return new NotFoundObjectResult(new { message = "Not found" });

            return new OkObjectResult(user);
        }

        [HttpGet("{id}/refresh-tokens")]
        public async Task<IActionResult> GetRefreshTokens(string id)
        {
            var user =  await _service.GetByID(id);
            if (user == null)
                return new NotFoundObjectResult(new { message = "Not found" });

            return new OkObjectResult(user.RefreshToken);
        }


        private string ipAddress()
        {
            if (_contextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _contextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
            else
                return _contextAccessor.HttpContext?.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
