using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntityFramework.Models
{
    public class Bank
    {
        public Bank() { }

        [Key]
        public int? BankID { get; set; }
        
        public string? BankIdentifier { get; set; }
        public string? BankName { get; set;}
        public string? HeadQuartersAddress { get; set; }
        public virtual ICollection<Location>? SubLocations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Users>? Users { get; set; }


        // CREATE BANK
        public Bank(string? bankname, string? headquarters)
        {
            BankName = bankname;
            HeadQuartersAddress = headquarters;
            BankIdentifier = bankname.Substring(0, 3).ToLower();
            SubLocations = new List<Location>();
            Users = new List<Users>();
        }

    }
}
