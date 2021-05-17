using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<string>> AddAsync(User user, string password);
        Task<List<string>> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync (User user, string password);
        Task<List<string>> DeleteAsync(User user);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllAsync(AppView appview);
        Task<List<string>> ResetPasswordAsync(User user, string token, string newPassword);
        Task<List<string>> UpdateAsync(User user);
    }
}