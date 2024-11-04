using BankAccount.WebAPI.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BankAccount.WebAPI.DAL
{
    public class BankAccountContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<AccountTransaction> AccountTransaction { get; set; }

        public BankAccountContext(DbContextOptions<BankAccountContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Accounts);
                //.WithOne(a => a.Customer)
                //.HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountTransactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);
        }
    }


}
