using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<PaginationList<T>> GetAllAsync(AppView appview, Expression<Func<T, bool>> predicate);
        Task<T> GetByKeyAsync(ulong id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(ulong id);
        Task DeleteAsync(T entity);
    }
}