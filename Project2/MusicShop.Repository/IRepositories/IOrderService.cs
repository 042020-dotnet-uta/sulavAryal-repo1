using MusicShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public interface IOrderService : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
