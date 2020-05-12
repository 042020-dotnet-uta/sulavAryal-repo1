using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using MusicShop.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly MSDbContext _context;

        public ProductRepository(MSDbContext context):base(context)
        {
            _context = context;
        }
    }
}
