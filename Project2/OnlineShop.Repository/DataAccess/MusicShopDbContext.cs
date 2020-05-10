using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;

namespace MusicShop.Repository.DataAccess
{
    public class MusicShopDbContext : DbContext
    {
        public MusicShopDbContext(DbContextOptions<MusicShopDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
