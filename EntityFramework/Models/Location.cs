using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
    public class Location
    {
        [Key] 
        public int? SubLocationID { get; set; }
        public string? SubLocationName { get; set; }
    }
}
