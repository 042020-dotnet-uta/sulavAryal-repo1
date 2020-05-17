using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.UI
{
    public class OrderService : GenericRepository<Order>, IOrderService
    {
        private readonly MSDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public OrderService(MSDbContext context, ShoppingCart shoppingCart) : base(context)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public async Task CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            _context.Orders.Add(order);
            var shoppinCartItems = _shoppingCart.ShoppingCartItems;
            foreach (var item in shoppinCartItems) 
            {
                var orderLineItem = new OrderLineItem 
                {
                    Quantity = item.Quantity,
                    InventoryItemId = item.Product.Id * order.StoreId,
                    OrderId = order.Id,
                    Price = item.Product.Price
                };
                _context.OrderLineItems.Add(orderLineItem);
            }
            await _context.SaveChangesAsync();
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
