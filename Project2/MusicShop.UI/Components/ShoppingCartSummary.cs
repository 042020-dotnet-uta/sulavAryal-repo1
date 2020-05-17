using Microsoft.AspNetCore.Mvc;
using MusicShop.Domain;
using MusicShop.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.UI.Components
{
	public class ShoppingCartSummary : ViewComponent
	{
		private readonly ShoppingCart _shoppingCart;
		public ShoppingCartSummary(ShoppingCart shoppingCart)
		{
			_shoppingCart = shoppingCart;
		}

		public IViewComponentResult Invoke()
		{
			var items = _shoppingCart.GetShoppingCartItems();
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
