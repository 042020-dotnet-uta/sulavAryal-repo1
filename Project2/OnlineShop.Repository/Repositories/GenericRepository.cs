using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public async Task<T> Add(T entity)
        {
            throw new NotImplementedException();
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


        public async Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
