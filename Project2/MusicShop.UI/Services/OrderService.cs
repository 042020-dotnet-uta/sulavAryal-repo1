using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.UI
{
    public class OrderService : GenericRepository<Order>, IOrderService
    {
        private readonly MSDbContext _context;
        private readonly ShoppingCart _shoppingCart;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(MSDbContext context, ShoppingCart shoppingCart, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _shoppingCart = shoppingCart;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateOrder(Order order)
        {
            try
            {
                order.OrderDate = DateTime.Now;
                _context.Orders.Add(order);
                var shoppinCartItems = _shoppingCart.ShoppingCartItems;
                foreach (var item in shoppinCartItems)
                {
                    var orderLineItem = new OrderLineItem
                    {
                        Order = order,
                        Quantity = item.Quantity,
                        InventoryItemId = item.Product.Id * order.StoreId,
                        Price = item.Product.Price
                    };

                    var inventoryItems = _context.Inventory
                      .Include(i => i.Product)
                      .Include(i => i.Store)
                      .AsNoTracking()
                      .Where(i => i.StoreId == order.StoreId && i.ProductId == item.Product.Id).FirstOrDefault();
                    var newQuantity = inventoryItems.Quantity - item.Quantity;

                    var inventoryToUpdate = new InventoryItem
                    {
                        Id = inventoryItems.Id,
                        ProductId = item.Product.Id,
                        StoreId = order.StoreId,
                        Quantity = newQuantity,
                        ChangedDate = DateTimeOffset.Now,
                        LoggedUserId = order.CustomerId
                    };

                    _context.OrderLineItems.Add(orderLineItem);
                    _context.Update(inventoryToUpdate);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderLineItems)
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<Order> GetOrderById(int? id)
        {
            if (id == null) 
            {
                return null;
            }
            try
            {
                return await _context.Orders
               .Include(o => o.OrderLineItems)
               .Include(o => o.Customer)
               .Include(o => o.Store)
               .AsNoTracking()
               .Where(o=>o.Id == id)
               .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

       
    }
}
