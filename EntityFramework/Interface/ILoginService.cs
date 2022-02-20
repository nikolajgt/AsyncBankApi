using EntityFramework.Models;
using EntityFramework.Models.JWT;

namespace EntityFramework.Interface
{
    public interface ILoginService
    {
        //Gets refresh and jwt from user login
        Task<AuthenticateResponse> Authenticate(Users user, string ipaddress);

        //Sets active token to not active and gets new refresh and jwt
        Task<AuthenticateResponse> RefreshToken(string token, string ipaddress);

        //Sets active token to not active
        Task<bool> RevokeToken(string token, string ipadress);
        Task<Users> GetByID(string id);
    }
}
