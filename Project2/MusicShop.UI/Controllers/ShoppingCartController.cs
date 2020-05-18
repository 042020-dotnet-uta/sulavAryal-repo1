using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Repository;
using MusicShop.UI.ViewModel;

namespace MusicShop.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            int? storeId = 0;

            if (TempData.ContainsKey("storeId"))
            {
                storeId = TempData["storeId"] as int?;
                ViewBag.StoreId = storeId;
            }
            // returns shopping cart items.
            var username = User.FindFirstValue(ClaimTypes.Name);
            var store = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _shoppingCart.GetShoppingCartItems(username);
            _shoppingCart.ShoppingCartItems = items;
            ViewBag.UserName = username;
            ViewBag.StoreId = Convert.ToInt32(store);
            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                StoreId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier))
        };
            return View(shoppingCartVM);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int id, int storeId = 0)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var store = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserName = username;
            ViewBag.StoreId = Convert.ToInt32(store);
            var selectedProduct = await _productRepository.FindSingleAsync(p => p.Id == id);
            if (selectedProduct != null)
            {
                TempData["storeId"] = storeId;
                _shoppingCart.AddToCart(selectedProduct, 1, username, storeId.ToString());
            }
            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int id)
        {
            var selectedProduct = await _productRepository.FindSingleAsync(p => p.Id == id);
            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }
    }
}