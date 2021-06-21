using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Models.Users;

namespace fooddelivery.Service.Interfaces
{
    public interface IUserService
    {
        Task<StringBuilder> AddAsync(User user, string password);
        Task<StringBuilder> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync (User user, string password);
        Task<StringBuilder> DeleteAsync(string id);
        Task<StringBuilder> DeleteAsync(User user);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllAsync(AppView appview);
        Task<StringBuilder> ResetPasswordAsync(User user, string token, string newPassword);
        Task<StringBuilder> UpdateAsync(User user);
    }
}