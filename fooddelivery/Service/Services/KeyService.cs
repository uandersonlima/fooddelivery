using System;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Access;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Users;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class KeyService : IKeyService
    {
        private readonly IKeyRepository _codigoRepos;
        private readonly IEmailService _emailSvc;

        public KeyService(IKeyRepository codigoRepos, IEmailService emailSvc)
        {
            _codigoRepos = codigoRepos;
            _emailSvc = emailSvc;
        }

        public async Task AddAsync(AccessKey accessKey)
        {
            var previousCode = await SearchKeyByEmailAndTypeAsync(accessKey.Email, accessKey.KeyType);
            if (!(previousCode is null))
            {
                await DeleteAsync(previousCode);
            }
            await _codigoRepos.AddAsync(accessKey);
        }

        public async Task CreateNewKeyAsync(User user, string keyType)
        {
            var rdn = new Random();
            var key = string.Empty;

            for (int i = 0; i < 6; i++)
                key += rdn.Next(9);

            AccessKey newKey = new AccessKey
            {
                Key = key,
                KeyType = keyType,
                DataGerada = DateTime.UtcNow,
                Email = user.Email
            };

            if (keyType == KeyType.Verification)
                _emailSvc.SendEmailVerificationAsync(user, key);
            else if (keyType == KeyType.Recovery)
                _emailSvc.SendEmailRecoveryAsync(user, key);

            await AddAsync(newKey);
        }

        public async Task DeleteAsync(AccessKey accessKey) => await _codigoRepos.DeleteAsync(accessKey);

        public async Task<TimeSpan> ElapsedTimeAsync(AccessKey accessKey)
        {
            var previousCodigo = await SearchKeyByEmailAndTypeAsync(accessKey.Email, accessKey.KeyType);
            if (!(previousCodigo is null))
            {
                return DateTime.UtcNow.Subtract(previousCodigo.DataGerada);
            }
            return new TimeSpan(0, 15, 0);
        }

        public async Task<AccessKey> SearchKeyByEmailAndTypeAsync(string email, string keyType)
        =>
        await _codigoRepos.SearchKeyByEmailAndTypeAsync(email, keyType);

        public async Task<AccessKey> SearchKeyAsync(string email, string key, string keyType)
        =>
        await _codigoRepos.SearchKeyAsync(email, key, keyType);
    }
}