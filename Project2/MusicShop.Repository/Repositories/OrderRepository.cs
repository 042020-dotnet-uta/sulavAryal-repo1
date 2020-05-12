using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using MusicShop.Repository.IRepositories;

namespace MusicShop.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly MSDbContext _context;

        public OrderRepository(MSDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
