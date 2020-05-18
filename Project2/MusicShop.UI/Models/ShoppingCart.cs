using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MusicShop.UI
{
    public class ShoppingCart
    {
        private readonly MSDbContext _context;

        public ShoppingCart(MSDbContext context)
        {
            _context = context;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        /// <summary>
        ///  returns ShoppingCart object which contains dbcontext and cartId Session.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            // Bring in the session
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            // Gets the dbcontext
            var context = services.GetService<MSDbContext>();
            // check to see if we already have the cartId in there 
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            // store that session 
            session.SetString("CartId", cartId);
            // returns ShoppingCart object which contains our context cartId Session.
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        /// <summary>
        /// Checks if shopping cart contains anything if it does 
        /// adds to the quantity otherwise adds as new.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        public void AddToCart(Product product, int amount, string username, string storeId)
        {
            var shoppingCartItem =
                    _context.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Quantity = 1,
                    customerEmail = username,
                    StoreId = storeId

                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Checks if shopping cart has more than one item
        /// if it has then decrease by one otherwise remove
        /// the shopping cart item.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem =
                    _context.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    localAmount = shoppingCartItem.Quantity;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();

            return localAmount;
        }
        /// <summary>
        /// Gets all the shopping cart items 
        /// </summary>
        /// <returns></returns>
        public List<ShoppingCartItem> GetShoppingCartItems(string username)
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId && c.customerEmail == username)
                           .Include(s => s.Product)
                           .ToList());
        }

        /// <summary>
        /// Gets all the shopping cart items 
        /// and removes it. 
        /// </summary>
        public void ClearCart()
        {
            var cartItems = _context
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _context.ShoppingCartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        /// <summary>
        /// Gives total amount 
        /// </summary>
        /// <returns></returns>
        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Quantity).Sum();
            return total;
        }
    }
}
