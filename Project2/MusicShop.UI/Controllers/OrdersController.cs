﻿using Microsoft.AspNetCore.Mvc;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.UI.ViewModel;
using System;
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
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else if(id.HasValue)
            {
                ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
                var storeOrders = await _orderService.GetOrdersByStore(id);
                return View(storeOrders);
            }
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
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
                        return RedirectToAction("Index", "Home", new { fromCheckout = true, isSuccess = false });
                    }
                }
            }
            _shoppingCart.ShoppingCartItems = items;
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                return RedirectToAction("Index", "Home", new { fromCheckout = true, isSuccess = false, emptyCart = true });
            }

            if (ModelState.IsValid)
            {
                await _orderService.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("Index", "Home", new { fromCheckout = true, isSuccess = true });
            }

            //return View(order);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }
            // bring in orders and order line items
            var order = await _orderService.GetOrderById(id);
            var products = await _productRepository.GetAllAsync();
            var orderDetailVM = new OrderDetailViewModel 
            {
                Order = order,
                Products = products
            };
           
            return View(orderDetailVM);
        }

        public async Task<IActionResult> CustomerOrder(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            else if (id.HasValue)
            {
                var storeOrders = await _orderService.GetOrdersByCustomer(id);
                return View("Index",storeOrders);
            }
            var orders = _orderService.GetAllOrders();
            return View("Index",orders);
        }
    }
}
