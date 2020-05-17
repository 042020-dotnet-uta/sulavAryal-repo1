using MusicShop.Domain;
using MusicShop.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.UI
{
    public interface IOrderService : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task CreateOrder(Order order);
    }
}
