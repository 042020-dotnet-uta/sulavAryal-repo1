using MusicShop.Domain;
using MusicShop.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.UI
{
    /// <summary>
    /// IOrderService that OrderSerive implements 
    /// </summary>
    public interface IOrderService : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int? id);
        Task<IEnumerable<Order>> GetOrdersByStore(int? id);
        Task<IEnumerable<Order>> GetOrdersByCustomer(int? id);
        Task CreateOrder(Order order);
    }
}
