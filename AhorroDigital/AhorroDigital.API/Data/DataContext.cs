using AhorroDigital.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AhorroDigital.API.Data
{
    public class DataContext : IdentityDbContext<User>  
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<DocumentType>DocumentTypes { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<LoanType> LoanTypes { get; set; }

        public DbSet<SavingType> SavingTypes { get; set; }

        public DbSet<Saving> Savings { get; set; }

        public DbSet<Contribute> Contributes { get; set; }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<PaymentPlan> PaymentPlan { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Retreat> Retreats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DocumentType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<AccountType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<LoanType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<SavingType>().HasIndex(x => x.Name).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(false);
        }
    }
}
