using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using MusicShop.UI.ViewModel;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicShop.UI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrdersController(IOrderService orderService, ShoppingCart shoppingCart,
            IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _orderService = orderService;
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }


        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }

        public async Task<ViewResult> ProductList()
        {
            var products = await _productRepository.GetAllAsync();
            ProductListViewModel pvm = new ProductListViewModel();
            pvm.Products = await _productRepository.GetAllAsync();

            return View(products);
        }

        public async Task<IActionResult> Checkout(int storeId)
        {
            var custResult = await _customerRepository.FindSingleAsync(c => c.Email == User.FindFirstValue(ClaimTypes.Name));
            //var storeResult = await 
            Order order = new Order
            {
                CustomerId = custResult.Id,
                StoreId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                OrderDate = DateTimeOffset.Now,
            };
            var username = User.FindFirstValue(ClaimTypes.Name);
            var inventory = await _productRepository.GetStoreInventory(storeId);
            var items = _shoppingCart.GetShoppingCartItems(username);
            
            foreach (var product in inventory)
            {
                foreach (var item in items)
                {
                    if (item.Product.Id == product.ProductId && item.Quantity > product.Quantity) 
                    {
                        ModelState.AddModelError("", $"Sorry we don't have {item.Quantity} of {item.Product.Name} right now ");
                        _shoppingCart.ClearCart();
                        return RedirectToAction("Index","Home", new { fromCheckout = true, isSuccess = false });
                    }
                }
            }
            _shoppingCart.ShoppingCartItems = items;
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                await _orderService.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("Index","Home",new { fromCheckout = true, isSuccess = true});
            }

            //return View(order);
            return RedirectToAction("Index", "Home");
        }
    }

}
