using Microsoft.EntityFrameworkCore;
using NextTech.PayHub.Accounting.Domain;
namespace NextTech.PayHub.Accounting.Infrastructure.EF
{  
    public class AccountingDBContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public AccountingDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmericanExpress>();
            modelBuilder.Entity<Visa>();
            modelBuilder.Entity<MasterCard>();        

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingDBContext).Assembly);
        }
    }
}
