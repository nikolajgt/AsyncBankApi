using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntityFramework.Models
{
    public class Loans
    {
        public Loans() { }

        [Key]
        public int? LoanID { get; set; }
        public DateTime? Issued { get; set; }
        public DateTime? Expire { get; set; }
        public int? PayBackBalance { get; set; }
        public int? TotalLoanOfMoney { get; set; }
        public string? UserID { get; set; }

        [ForeignKey("UserID")]
        [JsonIgnore]
        public virtual Users? Userid { get; set; }

        public Loans(int balance, int LoanMonths)             
        {
            Issued = DateTime.Now;
            Expire = DateTime.Now.AddMonths(LoanMonths);
            PayBackBalance = balance;
            TotalLoanOfMoney = balance;
        }
    }
}