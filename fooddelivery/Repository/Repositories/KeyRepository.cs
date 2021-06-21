using System.Linq;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models.Access;
using fooddelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class KeyRepository : IKeyRepository
    {
        private readonly FoodDeliveryContext _context;

        public KeyRepository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AccessKey accessKey)
        {
            await _context.AccessKeys.AddAsync(accessKey);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccessKey accessKey)
        {
            _context.AccessKeys.Remove(accessKey);
            await _context.SaveChangesAsync();
        }

        public async Task<AccessKey> SearchKeyAsync(string email, string key, string keytype)
        {
            return await _context.AccessKeys.AsNoTracking()
                        .Where(ak => ak.Email.Trim().ToLower() == email.Trim().ToLower() && ak.Key == key && ak.KeyType == keytype)
                        .FirstOrDefaultAsync();
        }
        public async Task<AccessKey> SearchKeyByEmailAndTypeAsync(string email, string keytype)
        {
            return await _context.AccessKeys.AsNoTracking().
                        Where(ak =>  ak.Email.Trim().ToLower() == email.Trim().ToLower()  &&  ak.KeyType == keytype).FirstOrDefaultAsync();
        }
    }
}