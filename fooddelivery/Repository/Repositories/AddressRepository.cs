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
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly FoodDeliveryContext _context;

        public AddressRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<PaginationList<Address>> GetAllByUserIdAsync(ulong userId, AppView appview)
        {
            var pagList = new PaginationList<Address>();
            var result = _context.Addresses.Where(address => address.UserId == userId && !address.isDeleted)
                                                 .AsNoTracking().AsQueryable();

            if (appview.CheckSearch())
            {
                result = result.Where(address => address.City.Contains(appview.Search) || address.State.Contains(appview.Search) || address.Neighborhood.Contains(appview.Search));
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