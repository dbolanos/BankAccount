using BankAccountAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
