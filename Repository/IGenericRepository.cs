using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace myFirtsAzureWebApp.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> LoadAll();
        Task<bool> InsertAsync(T entitu);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleterAsync(T entity);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, IEnumerable<string> inclutions);
        Task<T> GetAsync(IEnumerable<string> inclutions);
        Task<T> GetFirtsAsync(Expression<Func<T, bool>> filter);
        Task DeleteAsync(IEnumerable<T> entity);
    }
}
