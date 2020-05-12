using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using System;
using System.Collections.Generic;
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
    }
}
