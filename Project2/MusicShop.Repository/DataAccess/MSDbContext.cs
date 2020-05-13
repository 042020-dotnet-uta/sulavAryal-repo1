using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;

namespace MusicShop.Repository.DataAccess
{
    public class MSDbContext : DbContext
    {
        public MSDbContext(DbContextOptions<MSDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<InventoryItem> Inventory { get; set; }
    }
}
