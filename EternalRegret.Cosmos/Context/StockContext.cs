using EternalRegret.Cosmos.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace EternalRegret.Cosmos.Context
{
    public class StockContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var accountEndPoint = Environment.GetEnvironmentVariable("AccountEndPoint");
            var accountKey = Environment.GetEnvironmentVariable("AccountKey");
            var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

            optionsBuilder.UseCosmos(accountEndPoint, accountKey, databaseName);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().OwnsMany(s => s.Prices);
        }
    }
}
