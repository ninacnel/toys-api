using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<OrderLine>()
            .HasKey(ol => new { ol.OrderId, ol.OrderLineId }); // Define composite primary key

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Toy> toys { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<PriceHistory> priceHistories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderLine> ordersLines { get; set; }    
    }
}