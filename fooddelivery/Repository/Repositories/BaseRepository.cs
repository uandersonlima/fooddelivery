using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly FoodDeliveryContext _context;

        public BaseRepository(FoodDeliveryContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<PaginationList<T>> GetAllAsync(AppView appview, Expression<Func<T, bool>> predicate)
        {
            var pagList = new PaginationList<T>(); 
            var result = _context.Set<T>().AsNoTracking().AsQueryable();

    
            if (appview.CheckSearch() || appview.CheckDate())
            {
                result = result.Where(predicate);
            }
            if (appview.CheckPagination())
            {
                var totalRecords  = await result.CountAsync();
                result = result.Skip((appview.NumberPag.Value - 1) * appview.RecordPerPage.Value).Take(appview.RecordPerPage.Value);

                var pagination = new Pagination
                {
                    NumberPag = appview.NumberPag.Value,
                    RecordPerPage = appview.RecordPerPage.Value,
                    TotalRecords = totalRecords ,
                    TotalPages = (int)Math.Ceiling((double)totalRecords  / appview.RecordPerPage.Value)
                };

                pagList.Pagination = pagination;
            }
            pagList.AddRange(await result.ToListAsync());
            return pagList;
        }

        public virtual async Task<T> GetByKeyAsync(ulong id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}