using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicShop.Repository;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicShop.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Injects dependencies through DI container into this constructor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="logger"></param>
        public AccountController(ICustomerRepository customerRepository, ILogger<CustomerRepository> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public ILogger<CustomerRepository> _logger { get; }

        /// <summary>
        /// Index method returns the Index view. 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Index");

        }

        /// <summary>
        /// Checks for username, password and sets cookies 
        /// </summary>
        /// <param name="txtUserName"></param>
        /// <param name="txtPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string txtUserName, string txtPassword)
        {

            var result = await _customerRepository.ValidateCustomer(txtUserName, txtPassword);
            if (result == true)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,txtUserName),
                    new Claim(ClaimTypes.Email,txtUserName)
                };
                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme
                );
                var principle = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, props).Wait();
                ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
                return RedirectToAction("ChooseStore", "Home");
            }
            _logger.LogInformation("Login attempt unsuccessful");
            return View("Index");
        }

        /// <summary>
        /// Gets invoked when user logs out. 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _logger.LogInformation($"Bye bye {User.FindFirstValue(ClaimTypes.Name)}");
            return RedirectToAction("Index");
        }


    }
}