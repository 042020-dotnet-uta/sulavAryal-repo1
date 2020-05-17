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
            // returns shopping cart items.
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartVM);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int id)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var selectedProduct = await _productRepository.FindSingleAsync(p => p.Id == id);
            if (selectedProduct != null)
            {
               
                _shoppingCart.AddToCart(selectedProduct, 1, username);
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