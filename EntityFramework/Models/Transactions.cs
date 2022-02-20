using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntityFramework.Models
{
    public class Transactions
    {
        public Transactions() { }
        [Key]
        public int Id { get; set; }
        public string? money { get; set; }
        public string? balance { get; set; }
        public string? reciver { get; set; }
        public DateTime date { get; set; }
        public string? userID { get; set; }
        
        [ForeignKey("userID")]
        [JsonIgnore]
        public virtual Users? user { get; set; }

        public Transactions(int? startBalance, int? moneyIn, string? recivier, string? userid)
        {
            balance = startBalance.ToString();
            money = moneyIn.ToString();
            this.reciver = recivier;
            date = DateTime.Now;
            userID = userid;
        }
    }
}
