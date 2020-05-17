using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public OrdersController(IOrderService orderService, ShoppingCart shoppingCart, IProductRepository productRepository)
        {
            _orderService = orderService;
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
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

        

       
    }

}
