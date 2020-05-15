using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly MSDbContext _context;

        public CustomerRepository(MSDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Customer> FindCustomerById(int? id)
        {
            try
            {
                return await _context.Customers
                    .Include(c => c.CustomerAddress)
                    .AsNoTracking()
                    .Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
