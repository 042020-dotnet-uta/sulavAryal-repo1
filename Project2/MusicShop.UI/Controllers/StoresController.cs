using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using MusicShop.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
                .Include(s => s.Products)
                .ToListAsync());
        }

        public async Task<IActionResult> Inventory(int? id, bool isSuccess = false)
        {

            if (id == null)
            {
                return NotFound();
            }
            ViewBag.IsSuccess = isSuccess;
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await _productRespository.GetStoreInventory(id);
            var store = await _context.Stores.Where(s => s.Id == id).FirstOrDefaultAsync();
            TempData["storeId"] = store.Id;

            #region adds storeId to the cookie
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,User.FindFirstValue(ClaimTypes.Name)),
                    new Claim(ClaimTypes.NameIdentifier,store.Id.ToString())
                };
            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme
            );
            var principle = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, props).Wait();
            #endregion

            ViewBag.Store = store.Name;
            if (result == null)
            {
                return NotFound();
            }
            var storeInVM = new StoreInventoryViewModel
            {
                InventoryItems = result,
                IsSuccess = ViewBag.IsSuccess
            };
            return View("Inventory", storeInVM);
        }

        public async Task<IActionResult> AddInventory(int? id)
        {
            ViewBag.StoreId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await _productRespository.GetProductQuantity(Convert.ToInt32(ViewBag.StoreId), id);

            return View("AddInventory", result);
        }


        public async Task<IActionResult> UpdateInventory(int storeId, int? id, int quantity)
        {
            var result = await _productRespository.GetProductQuantity(storeId, id);
            // you can only add into inventory from here, can't subtract. 
            ViewBag.StoreId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            result.Quantity = quantity + result.Quantity;
            await _productRespository.AddInventoryItems(result);

            var storeInv = await _productRespository.GetStoreInventory(Convert.ToInt32(ViewBag.StoreId));
            var storeInVM = new StoreInventoryViewModel
            {
                InventoryItems = storeInv,
                IsSuccess = true
            };
            return View("Inventory", storeInVM);
        }


        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
