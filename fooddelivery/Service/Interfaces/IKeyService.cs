using System;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Access;
using fooddelivery.Models.Constants;

namespace fooddelivery.Service.Interfaces
{
    public interface IKeyService
    {
        Task AddAsync(AccessKey acessKey);
        Task CreateNewKeyAsync(User user, string keytype);
        Task DeleteAsync(AccessKey acessKey);
        Task<TimeSpan> ElapsedTimeAsync(AccessKey acessKey);
        Task<AccessKey> SearchKeyAsync(string key, string email, string keytype);
        Task<AccessKey> SearchKeyByEmailAndTypeAsync(string email, string keytype);
    }
}