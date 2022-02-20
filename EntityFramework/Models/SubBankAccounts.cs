using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
    public class SubBankAccounts
    {
        public SubBankAccounts() { }

        [Key]
        public int? SubBankAccountID { get; set; }
        public string? SubAccountName { get; set; }
        public int? Balance { get; set; }
        public SubBankAccountType SubBankAccountType { get; set; }

        public SubBankAccounts(string subaccount, int balance, SubBankAccountType type) 
        {
            SubAccountName = subaccount;
            Balance = balance;
            SubBankAccountType = type;
        }
    }

    public enum SubBankAccountType { Opsaringskonto, Hverdagskonto, Pensionskonto }
}
