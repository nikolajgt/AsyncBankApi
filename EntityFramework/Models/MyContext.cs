using EntityFramework.Models.JWT;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<SubBankAccounts> SubBankAccounts { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<Rki> Rki { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        //public DbSet<RefreshToken> RefreshToken { get; set; }

        public MyContext(DbContextOptions options) : base(options) { }
    }
}
