using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task<PaginationList<T>> GetAllAsync(AppView appview, Expression<Func<T, bool>> predicate)
        {
            return await _repository.GetAllAsync(appview, predicate);
        }

        public async Task<T> GetByKeyAsync(ulong id)
        {
            return await _repository.GetByKeyAsync(id);
        }

        public async Task DeleteAsync(ulong id)
        {
            await DeleteAsync(await GetByKeyAsync(id));
        }
        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}