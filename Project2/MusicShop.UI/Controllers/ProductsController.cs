using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository;
using MusicShop.Repository.DataAccess;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicShop.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MSDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public ProductsController(MSDbContext context, IProductRepository productRepository,
            ICustomerRepository customerRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Serves up Product Contorllers' index view with appropriate model. 
        /// </summary>
        /// <returns></returns>
        // GET: Products
        public async Task<IActionResult> Index()
        {
            
            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            if (TempData["isSuccess"] != null)
            {
                bool isSuccess = (bool)TempData["isSuccess"];
                ViewBag.isSuccess = isSuccess == true ? true: false ;
            }
           
            var result = await _productRepository.GetAllAsync();
            return View(result);
        }

        /// <summary>
        /// Shows details about the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// View to go to, to create product
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// This is where you create a new product from.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProductCode,Price")] Product product)
        {
            if (ModelState.IsValid)
            {

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        /// <summary>
        /// This is where you reach if you try to create a new product first.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// This is where product can get its details updated. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProductCode,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        /// <summary>
        /// This is where product gets deleted. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        /// <summary>
        /// This is where request comes to when user confirms product deletion. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// This a check to see if product exists. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }


    }
}
