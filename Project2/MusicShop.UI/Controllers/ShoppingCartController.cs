using Microsoft.AspNetCore.Mvc;
using MusicShop.Repository;
using MusicShop.UI.ViewModel;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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
        /// <summary>
        /// Serves up a page that shows shopping cart. 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Adds items to the shoppincart. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes items form the shopping cart if user chooses to. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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