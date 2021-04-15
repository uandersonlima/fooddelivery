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

        public async Task<T> GetByKeyAsync(int code)
        {
            return await _repository.GetByKeyAsync(code);
        }

        public async Task RemoveAsync(int code)
        {
            await RemoveAsync(await GetByKeyAsync(code));
        }
        public async Task RemoveAsync(T entity)
        {
            await _repository.RemoveAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}