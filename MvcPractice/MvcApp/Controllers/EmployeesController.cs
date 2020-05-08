using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcApp.DataAccess;
using MvcApp.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp
{
    public class EmployeesController : Controller
    {
        private readonly MvcAppContext _context;
        public EmployeesController(MvcAppContext mvcAppContext)
        {
            this._context = mvcAppContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.Include(e => e.Department).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            PopulateDropDownLists();
            ViewBag.DepartmentId = new SelectList(await _context.Departments.ToListAsync(), nameof(Department.Id), nameof(Department.Name));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, DepartmentId")] Employee employee)
        {

          
            if (ModelState.IsValid)
            {
                employee.Department = _context.Departments.Find(employee.DepartmentId);
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DepartmentId = new SelectList(_context.Departments, nameof(Department.Id), nameof(Department.Name));
            return View(employee);
        }

        private void PopulateDropDownLists(Employee employee = null)
        {
            ViewData["DoctorID"] = DepartmentSelectList(employee?.DepartmentId);
        }

        // This is a twist on the PopulateDropDownLists approach
        // Create methods that return each SelectList separately
        // and one method to put them all into ViewData
        // This approach allows for AJAX requests refresh 
        // DDL Data at a later date. 

        /// Passing in nullable int selectedId 
        /// used to indicate which if I want one of the items we selected
        /// in the dropdown list.!-- 
        private SelectList DepartmentSelectList(int? selectedId)
        {
            return new SelectList(_context.Departments
                .OrderBy(d => d.Name)
                , selectedId);
        }
    }
}