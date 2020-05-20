using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Domain;
using MusicShop.Repository;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicShop.UI.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        //public string SessionKeyName { get; private set; }

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Directs to Customer index page with model
        /// </summary>
        /// <param name="SearchString"></param>
        /// <param name="isSuccess"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Index(string SearchString, bool isSuccess = false)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            ViewData["Searching"] = ""; // Assume not searching at the begining. 
            var customers = await _customerRepository.GetAllAsync();

            // Search Customers
            if (!String.IsNullOrEmpty(SearchString))
            {
                customers = customers.Where(p => p.LastName.ToUpper().Contains(SearchString.ToUpper())
                                       || p.FirstName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Searching"] = " show";
            }
            return View(customers);
        }

        /// <summary>
        /// Serves Create View 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Creates a Customer using the posted data. 
        /// </summary>
        /// <param name="rePassword"></param>
        /// <param name="customer"></param>
        /// <param name="customerAddress"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string rePassword, [Bind("FirstName,LastName,Email,PhoneNo,Password")] Customer customer,
            [Bind("Street,City,State,Zip")] CustomerAddress customerAddress
        )
        {

            if (ModelState.IsValid)
            {
                var checkCustomer = await _customerRepository.FindSingleAsync(i => i.Email == customer.Email);
                if (checkCustomer == null)
                {
                    if (customer.Password != rePassword)
                    {
                        ModelState.AddModelError("Password", "Password's did't match.");
                        return View("Create", customer);
                    }
                    var cusAdd = new CustomerAddress
                    {
                        Street = customerAddress.Street,
                        City = customerAddress.City,
                        State = customerAddress.State,
                        Zip = customerAddress.Zip
                    };
                    var cust = new Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        PhoneNo = customer.PhoneNo,
                        CustomerAddress = cusAdd,
                        Password = customer.Password,
                        UserTypeId = 2
                    };

                    await _customerRepository.AddAsync(cust);
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "Customer with same email address already exits.");
                    return View(customer);
                }
            }
            return View(customer);
        }


        /// <summary>
        /// Displays the details about user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var customer = await _Customers
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var customer = await _customerRepository.FindSingleAsync(i => i.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        /// Get view for Edit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.FindCustomerById(id);
            //var cust = customer.Include(i=>i.CustomerAddress).
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Uses the posted data to edit customer details. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <param name="customerAddress"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNo")] Customer customer,
            [Bind("Street,City,State,Zip")] CustomerAddress customerAddress)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.CustomerAddress = customerAddress;
                    await _customerRepository.UpdateAsync(customer);
                }
                catch (Exception)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


       /// <summary>
       /// Deletes the customer using the id passed in.
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.FindSingleAsync(i => i.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        /// Confirms with user about delete before actually deleting it from
        /// database. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
