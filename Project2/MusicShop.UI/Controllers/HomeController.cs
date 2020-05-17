using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicShop.Repository;
using MusicShop.UI.Models;
using MusicShop.UI.ViewModel;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<ViewResult> Index() 
        {
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await _productRepository.GetAllAsync();
            var homeViewModel = new HomeViewModel
            {
                Products = result
            };
            
            return View(homeViewModel);
        }
       
    }
}
