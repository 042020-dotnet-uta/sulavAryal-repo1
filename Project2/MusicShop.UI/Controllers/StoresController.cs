using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;

namespace MusicShop.UI.Controllers
{
    public class StoresController : Controller
    {
        private readonly MSDbContext _context;
        private readonly IProductRepository _productRespository;

        public StoresController(MSDbContext context, IProductRepository productRespository)
        {
            _context = context;
            _productRespository = productRespository;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stores
                .Include(s=>s.Products)
                .ToListAsync());
        }

        public async Task<IActionResult> Inventory(int? id) 
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productRespository.GetStoreInventory(id);
            var store = await _context.Stores.Where(s => s.Id == id).FirstOrDefaultAsync();
            TempData["storeId"] = store.Id;
            ViewBag.Store = store.Name;
            if (result == null)
            {
                return NotFound();
            }
            return View("Inventory",result);
        }

        public async Task<IActionResult> AddInventory(int? id)
        {
            ViewBag.StoreId = Convert.ToInt32(TempData["storeId"]);
            var result = await _productRespository.GetProductQuantity(ViewBag.StoreId, id);
           
            return View("AddInventory", result);
        }


        public async Task<IActionResult> UpdateInventory(int storeId, int? id, int quantity)
        {
            var result = await _productRespository.GetProductQuantity(storeId, id);
            // you can only add into inventory from here, can't subtract. 

            result.Quantity = quantity + result.Quantity;
            await _productRespository.AddInventoryItems(result);
            return View("AddInventory", result);
        }


        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
