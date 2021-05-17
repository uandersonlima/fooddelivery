using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<PaginationList<T>> GetAllAsync(AppView appview, Expression<Func<T, bool>> predicate);
        Task<T> GetByKeyAsync(long id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}