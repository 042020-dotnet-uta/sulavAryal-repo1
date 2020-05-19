using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class HomeController : Controller
    {
        private readonly MSDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        public HomeController(MSDbContext context, ILogger<HomeController> logger,
            IProductRepository productRepository)
        {
            _context = context;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<ViewResult> Index(int id = 0, 
            bool fromCheckout = false, bool isSuccess = false, bool emptyCart = false)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.fromCheckout = fromCheckout;
            ViewBag.EmptyCart = emptyCart;
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            var StoreId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.StoreId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var cartTest = ShoppingCart.GetCart();
            int? storeId = 0;

            if (TempData.ContainsKey("storeId"))
            {


                storeId = TempData["storeId"] as int?;
                ViewBag.StoreId = StoreId;
            }
            var result = await _productRepository.GetStoreInventory(Convert.ToInt32(StoreId));
            var homeViewModel = new HomeViewModel
            {
                Products = result,
                StoreId = Convert.ToInt32(ViewBag.StoreId)
            };
            if (id != 0)
            {
                ViewBag.StoreId = id;

                result = await _productRepository.GetStoreInventory(id);
                homeViewModel = new HomeViewModel
                {
                    Products = result,
                    StoreId = Convert.ToInt32(StoreId)
                };
                homeViewModel.StoreId = id;
                return View(homeViewModel);
            }
            return View(homeViewModel);
        }

        public IActionResult ChooseStore()
        {
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name");
            return View("ChooseStore");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseStore(IFormCollection form)
        {
            var customer = User.FindFirstValue(ClaimTypes.Name);
            var formStoreId = form["StoreId"][1];
            if (string.IsNullOrEmpty(customer) || string.IsNullOrEmpty(formStoreId)) 
            {
                return RedirectToAction("Logout","Account");
            }
            if (form["StoreId"][1] != "Choose a location")
            {
                var result = await _productRepository.GetStoreInventory(Convert.ToInt32(form["StoreId"][1]));
                var store = await _context.Stores.Where(s => s.Id == Convert.ToInt32(form["StoreId"][1])).FirstOrDefaultAsync();

                #region adds storeId to the cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, customer),
                    new Claim(ClaimTypes.NameIdentifier, form["StoreId"][1])
                };
                if (claims == null) 
                {
                    NotFound();
                }
                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme
                );
                var principle = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, props).Wait();

                #endregion
                ViewBag.StoreId = store.Id;
                ViewBag.Store = store.Name;
                if (result == null)
                {
                    return NotFound();
                }

                TempData["storeId"] = Convert.ToInt32(form["StoreId"][1]);
                return RedirectToAction("Index");
            }

            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name");
            ModelState.AddModelError("", "Choose a store location");
            return View("ChooseStore");
        }

    }
}
