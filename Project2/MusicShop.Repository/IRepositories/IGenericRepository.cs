using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FindSingle(Expression<Func<T, bool>> predicate);
        Task Delete(int? id);
        void SaveChanges();
    }
}
