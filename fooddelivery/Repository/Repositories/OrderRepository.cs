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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly FoodDeliveryContext _context;

        public OrderRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override async Task<PaginationList<Order>> GetAllAsync(AppView appview, Expression<Func<Order, bool>> predicate)
        {
            var pagList = new PaginationList<Order>();
            var result = _context.Set<Order>()
                                 .Include(order => order.User)
                                 .Include(order => order.Address)
                                 .Include(order => order.Suborders)
                                 .ThenInclude(sub => sub.Food)
                                 .ThenInclude(food => food.Images)
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
            pagList.AddRange(await result.ToListAsync());
            return pagList;
        }
        public async Task<PaginationList<Order>> GetAllByUserIdAsync(ulong userId, AppView appview)
        {
            var pagList = new PaginationList<Order>();
            var result = _context.Orders.Where(order => order.UserId == userId)
                                                .Include(order => order.User)
                                                .Include(order => order.Address)
                                                .Include(order => order.Suborders)
                                                .ThenInclude(sub => sub.Food)
                                                .ThenInclude(food => food.Images)
                                                .AsNoTracking().AsQueryable();



            if (appview.CheckSearch())
            {
                result = result.Where(order => order.DeliveryStatus.Name.Contains(appview.Search) || order.Suborders.Any(sub => sub.Food.Name.Contains(appview.Search)));
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
        public async Task<PaginationList<Order>> GetAllByAddressIdAsync(ulong addressId, AppView appview)
        {
            var pagList = new PaginationList<Order>();
            var result = _context.Orders.Where(order => order.AddressId == addressId)
                                        .AsNoTracking().AsQueryable();

            if (appview.CheckSearch())
            {
                result = result.Where(order => order.DeliveryStatus.Name.Contains(appview.Search) || order.Suborders.Any(sub => sub.Food.Name.Contains(appview.Search)));
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