using Microsoft.EntityFrameworkCore;
using MusicShop.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MSDbContext _context;

        public GenericRepository(MSDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Add Entity of type T to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
                
            }
         
        }

        /// <summary>
        /// Gets all the record from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>()
             .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        /// <summary>
        /// Deletes record from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            try
            {
                var result = await _context.FindAsync<T>(id);
                _context.Set<T>().Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        /// <summary>
        /// Gets list of records from database based on predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>()
            .AsQueryable()
            .Where(predicate)
            .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
         
        }
        /// <summary>
        /// Gets list of records from database based on predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// Gets single record from database
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// Updates changes to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

            }

        }

        /// <summary>
        /// Saves changes to database
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
