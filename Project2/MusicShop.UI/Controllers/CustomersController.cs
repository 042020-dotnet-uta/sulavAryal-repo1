using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.UI.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public string SessionKeyName { get; private set; }

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
         
        }


        public async Task<IActionResult> Index(string SearchString)
        {
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,PhoneNo")] Customer customer, [Bind("Street,City,State,Zip")] CustomerAddress customerAddress
        )
        {
           
            if (ModelState.IsValid)
            {
                var cusAdd = new CustomerAddress 
                {
                    Street = customerAddress.Street,
                    City = customerAddress.City,
                    State= customerAddress.State,
                    Zip = customerAddress.Zip
                };
                var cust = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNo = customer.PhoneNo,
                    CustomerAddress = cusAdd,
                    UserTypeId = 2
                };
                await _customerRepository.AddAsync(cust);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        // GET: Customers/Details/5
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

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.FindSingleAsync(i => i.Id == id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _customerRepository.UpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CustomerExists(customer.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
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

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
     
    }
}
