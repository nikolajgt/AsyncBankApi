namespace EntityFramework.Models.DTO
{
    public class CreateuserDTO
    {
        public int? UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? ChessScore { get; set; }
    }
}
