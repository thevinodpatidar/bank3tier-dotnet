using System;
using Bank3Tier.Core.Models;
using Bank3Tier.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Bank3Tier.Data
{
    public class Bank3TierDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public Bank3TierDbContext(DbContextOptions<Bank3TierDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new UserConfiguration());

            builder
                .ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
