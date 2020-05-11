using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MusicShop.Repository.DataAccess;

namespace MusicShop.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MSDbContext _context;

        public GenericRepository(MSDbContext context)
        {
            _context = context;
        }
        
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            var result = SaveChanges();
        }

        public async Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindSingle(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public async Task Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChanges()
        {
             return await _context.SaveChangesAsync();
        }
    }
}
