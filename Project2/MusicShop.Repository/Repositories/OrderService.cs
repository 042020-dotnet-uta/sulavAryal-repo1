using MusicShop.Domain;
using MusicShop.Repository.DataAccess;

namespace MusicShop.Repository
{
    public class OrderService : GenericRepository<Order>, IOrderService
    {
        private readonly MSDbContext _context;

        public OrderService(MSDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
