namespace EntityFramework.Models.JWT
{
    public class AuthenticateResponse
    {
        public string? Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }

        public AuthenticateResponse(Users user, string jwt, string refreshtoken)
        {
            Id = user.UserID;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            JwtToken = jwt;
            RefreshToken = refreshtoken;
        }
    }
}
