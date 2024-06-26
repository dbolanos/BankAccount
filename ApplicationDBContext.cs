﻿using BankAccountAPI.Entities;
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

            //Unique AccountNumber column in Account table
            modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();

            //Relationships Configuration

            //RelationBetween Transaction and Account
            modelBuilder.Entity<Transaction>()
               .HasOne(t => t.Account)
               .WithMany(a => a.Transactions)
               .HasForeignKey(t => t.AccountId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Transaction>()
            .HasOne(t => t.ToAccount)
            .WithMany()
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
