using MusicShop.Domain;
using MusicShop.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.UI
{
    public interface IOrderService : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int? id);
        Task CreateOrder(Order order);
    }
}
