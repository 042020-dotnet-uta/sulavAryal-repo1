using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Domain;
using MusicShop.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicShop.UI.Components
{
	public class ShoppingCartSummary : ViewComponent
	{
		private readonly ShoppingCart _shoppingCart;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ShoppingCartSummary(ShoppingCart shoppingCart, IHttpContextAccessor httpContextAccessor)
		{
			_shoppingCart = shoppingCart;
			_httpContextAccessor = httpContextAccessor;
		}

		public IViewComponentResult Invoke()
		{

			var username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
			var items = _shoppingCart.GetShoppingCartItems(username);
						//  new List<ShoppingCartItem>() { new ShoppingCartItem(), new ShoppingCartItem() };
			_shoppingCart.ShoppingCartItems = items;

			var shoppingCartViewModel = new ShoppingCartViewModel
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};
			return View(shoppingCartViewModel);
		}
	}

}
