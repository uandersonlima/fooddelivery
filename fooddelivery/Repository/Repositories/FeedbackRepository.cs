using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class FeedbackRepository : BaseRepository<Feedbacks>, IFeedbackRepository
    {
        private readonly FoodDeliveryContext _context;

        public FeedbackRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override async Task<Feedbacks> GetByKeyAsync(ulong id)
        {
            return await _context.Feedbacks.FindAsync(null, id);
        }

        public override async Task<PaginationList<Feedbacks>> GetAllAsync(AppView appview, Expression<Func<Feedbacks, bool>> predicate)
        {
            var pagList = new PaginationList<Feedbacks>();
            var result = _context.Set<Feedbacks>()
                                 .Include(feed => feed.User)
                                 .Include(feed => feed.Order)
                                 .ThenInclude(order => order.Suborders)
                                 .ThenInclude(sub => sub.Food).AsNoTracking().AsQueryable();


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
            pagList.AddRange(await result.ToListAsync());
            return pagList;
        }


        public async Task<PaginationList<Feedbacks>> GetAllByUserIdAsync(ulong userId, AppView appview)
        {
            var pagList = new PaginationList<Feedbacks>();
            var result = _context.Feedbacks.Where(feed => feed.UserId == userId)
                                        .Include(feed => feed.User)
                                        .Include(feed => feed.Order)
                                        .ThenInclude(order => order.Suborders)
                                        .ThenInclude(sub => sub.Food)
                                        .AsNoTracking().AsQueryable();

            if (appview.CheckSearch())
            {
                result = result.Where(feed => feed.Note.Contains(appview.Search));
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