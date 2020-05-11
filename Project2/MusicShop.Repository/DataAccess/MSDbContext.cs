using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;

namespace MusicShop.Repository.DataAccess
{
    public class MSDbContext : DbContext
    {
        public MSDbContext(DbContextOptions<MSDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
