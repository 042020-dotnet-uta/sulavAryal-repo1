using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public class OrderService : GenericRepository<Order>, IOrderService
    {
        private readonly MSDbContext _context;

        public OrderService(MSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await  _context.Orders
                .Include(o => o.OrderLineItems)
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
