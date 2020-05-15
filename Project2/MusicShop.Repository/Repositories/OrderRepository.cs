using MusicShop.Domain;
using MusicShop.Repository.DataAccess;

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
