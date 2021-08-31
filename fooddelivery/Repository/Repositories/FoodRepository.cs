using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        private readonly FoodDeliveryContext _context;

        public FoodRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public override async Task<Food> GetByKeyAsync(ulong id)
        {
            return await _context.Foods
            .Include(food => food.Images)
            .Include(food => food.FoodIngredients)
            .FirstOrDefaultAsync(food => food.Id == id);
        }
        public override async Task<PaginationList<Food>> GetAllAsync(AppView appview, Expression<Func<Food, bool>> predicate)
        {
            var pagList = new PaginationList<Food>();
            var result = _context.Set<Food>()
                                 .Include(food => food.Images)
                                 .Include(food => food.Suborders)
                                 .Include(food => food.FoodIngredients)
                                 .AsNoTracking().AsQueryable();


            if (appview.CheckSearch() || appview.CheckDate())
            {
                result = result.Where(predicate);
            }

            if (appview.CheckPagination())
            {
                var totalRecords = await result.CountAsync();
                result = result.Skip((appview.NumberPag.Value - 1) * appview.RecordPerPage.Value).Take(appview.RecordPerPage.Value);

                var pagination = new Pagination
                {
                    NumberPag = appview.NumberPag.Value,
                    RecordPerPage = appview.RecordPerPage.Value,
                    TotalRecords = totalRecords,
                    TotalPages = (int)Math.Ceiling((double)totalRecords / appview.RecordPerPage.Value)
                };

                pagList.Pagination = pagination;
            }

            var list = await result.OrderByDescending(food => food.Suborders.Count()).ToListAsync();
            list.ForEach(food => food.Suborders = null);
            pagList.AddRange(list);
            return pagList;
        }

        public async Task<PaginationList<Food>> GetByCategoryIdAsync(ulong categoryId, AppView appview)
        {
            var pagList = new PaginationList<Food>();
            var result = _context.Foods.Where(food => food.CategoryId == categoryId/* && food.Available*/).AsNoTracking().AsQueryable();

            if (appview.CheckSearch())
            {
                result = result.Where(food => food.Name.Contains(appview.Search));
            }
            if (appview.CheckPagination())
            {
                var totalRecords = await result.CountAsync();
                result = result.Skip((appview.NumberPag.Value - 1) * appview.RecordPerPage.Value).Take(appview.RecordPerPage.Value);

                var pagination = new Pagination
                {
                    NumberPag = appview.NumberPag.Value,
                    RecordPerPage = appview.RecordPerPage.Value,
                    TotalRecords = totalRecords,
                    TotalPages = (int)Math.Ceiling((double)totalRecords / appview.RecordPerPage.Value)
                };

                pagList.Pagination = pagination;
            }
            pagList.AddRange(await result.ToListAsync());
            return pagList;
        }
    }
}