using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntityFramework.Models
{
    public class Rki
    {
        public Rki() { }

        [Key]
        public int? Id { get; set; }
        public int? LoanID { get; set; }
        public DateTime? Issued { get; set; }
        public DateTime? Expire { get; set; }
        public int? PayBackBalance { get; set; }
        public int? TotalLoanOfMoney { get; set; }
        public string? UserID { get; set; }

        [ForeignKey("UserID")]
        [JsonIgnore]
        public virtual Users? Userid { get; set; }


        public Rki(int? loanid, DateTime? issued, DateTime? expire, int? paybackbalance, int? totalloanofmoney, string? userid)
        {
            LoanID = loanid;
            Issued = issued;
            Expire = expire;
            PayBackBalance = paybackbalance;
            TotalLoanOfMoney = totalloanofmoney;
            UserID = userid;
        }
    }
}
