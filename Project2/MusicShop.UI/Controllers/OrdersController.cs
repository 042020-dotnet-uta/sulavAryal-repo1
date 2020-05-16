using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;

namespace MusicShop.UI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MSDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IProductRepository _productRepository;

        public OrdersController(MSDbContext context, IOrderService orderService, IProductRepository productRepository)
        {
            _context = context;
            _orderService = orderService;
            _productRepository = productRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name");

            //var vm = new OrderVM();
            //vm.Customers = new List<SelectListItem>
            //{
            //    new SelectListItem {Text = "Shyju", Value = "1"},
            //};
            //return View(vm);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            var customer = User.FindFirstValue(ClaimTypes.Name);
            if (form["StoreId"][1] != "Choose a location")
            {
                 //< a asp - action = "Inventory" asp - route - id = "@item.Id" > Inventory </ a >
                //CreateOrder(customer, form["StoreId"][1]);
                
                var result = await _productRepository.GetStoreInventory(Convert.ToInt32(form["StoreId"][1]));
                var store = await _context.Stores.Where(s => s.Id == Convert.ToInt32(form["StoreId"][1])).FirstOrDefaultAsync();
                ViewBag.StoreId = store.Id;
                ViewBag.Store = store.Name;
                if (result == null)
                {
                    return NotFound();
                }
                return View("Inventory", result);
            }

            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name");
            return View();
        }

        //public async Task<IActionResult> Inventory(int? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _productRepository.GetStoreInventory(id);
        //    var store = await _context.Stores.Where(s => s.Id == id).FirstOrDefaultAsync();
        //    ViewBag.StoreId = store.Id;
        //    ViewBag.Store = store.Name;
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return View("Inventory", result);
        //}

        public void CreateOrder(string customer, string storeId) 
        {
            
        }
    }

}
