using System.Threading.Tasks;
using fooddelivery.Models.Access;
using fooddelivery.Models.Constants;

namespace fooddelivery.Repository.Interfaces
{
    public interface IKeyRepository
    {
        Task AddAsync(AccessKey accessKey);
        Task DeleteAsync(AccessKey accessKey);
        Task<AccessKey> SearchKeyAsync(string key, string email, string keytype);
        Task<AccessKey> SearchKeyByEmailAndTypeAsync(string email, string keytype);
    }
}