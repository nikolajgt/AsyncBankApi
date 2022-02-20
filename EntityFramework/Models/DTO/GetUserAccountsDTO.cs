namespace EntityFramework.Models.DTO
{
    public class GetUserAccountsDTO
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? MainAccountBalance { get; set; }

        public virtual ICollection<SubBankAccounts>? SubBankAccounts { get; set; }
    }
}
