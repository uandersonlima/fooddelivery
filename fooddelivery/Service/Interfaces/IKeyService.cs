using System;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Access;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Users;

namespace fooddelivery.Service.Interfaces
{
    public interface IKeyService
    {
        Task AddAsync(AccessKey accessKey);
        Task CreateNewKeyAsync(User user, string keytype);
        Task DeleteAsync(AccessKey accessKey);
        Task<TimeSpan> ElapsedTimeAsync(AccessKey accessKey);
        Task<AccessKey> SearchKeyAsync(string key, string email, string keytype);
        Task<AccessKey> SearchKeyByEmailAndTypeAsync(string email, string keytype);
    }
}