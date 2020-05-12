using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
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
        
        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
               .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var result=  await _context.FindAsync<T>(id);
            _context.Set<T>().Remove(result);
           await _context.SaveChangesAsync();

          //  await _context.FindAsync<T>(id);
           // _context.Remove(ent);
           // await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
               .AsQueryable()
               .Where(predicate)
               .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }


        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
             return await _context.SaveChangesAsync();
        }
    }
}
