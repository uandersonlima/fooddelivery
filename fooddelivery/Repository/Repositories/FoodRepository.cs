using System;
using System.Linq;
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
        }

        public async Task<PaginationList<Food>> GetByCategoryIdAsync(long categoryId, AppView appview)
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