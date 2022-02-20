using EntityFramework.Models.JWT;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Models
{
    public class Users
    {
        public Users() { }

        [Key]
        public string? UserID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Balance { get; set; }
        public int? BankID { get; set; }
        

        [ForeignKey("BankID")]
        public virtual Bank Bank { get; set; }

        public virtual ICollection<SubBankAccounts>? SubBankAccounts { get; set; }
        public virtual ICollection<Loans>? Loans { get; set; }
        public virtual ICollection<Rki>? Rki { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual ICollection<RefreshToken>? RefreshToken { get; set; }


        public Users(string firstname, string lastname, string username, string password, int balance)
        {
            UserID = Guid.NewGuid().ToString();
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Password = password;
            Balance = balance;
            SubBankAccounts = new List<SubBankAccounts>();
            Loans = new List<Loans>();
            Rki = new List<Rki>();
        }

        public Users(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
