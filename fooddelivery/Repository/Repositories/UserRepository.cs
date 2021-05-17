using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        private StringBuilder GetErrors(IdentityResult response)
        {
            if (response.Succeeded)
            {
                return new StringBuilder();
            }
            else
            {
                var errors = new StringBuilder();
                foreach (var error in response.Errors)
                {
                    errors.Append(error.Code + ":\n" + error.Description);
                }
                return errors;
            }
        }

        public async Task<StringBuilder> AddAsync(User user, string password)
        {
            var response = await _userManager.CreateAsync(user, password);
            return GetErrors(response);
        }

        public async Task<StringBuilder> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var response = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return GetErrors(response);
        }
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<StringBuilder> DeleteAsync(User user)
        {
            var response = await _userManager.DeleteAsync(user);
            return GetErrors(response);
        }

        public async Task<List<User>> GetAllAsync(AppView appview)
        {
            var userList = _userManager.Users.AsNoTracking();

            if (appview.CheckSearch())
                userList = userList.Where(u => u.UserName.Contains(appview.Search));

            return await userList.ToListAsync();
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<StringBuilder> ResetPasswordAsync(User user, string token, string newPassword)
        {
            var response = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return GetErrors(response);
        }

        public async Task<StringBuilder> UpdateAsync(User user)
        {
            var response = await _userManager.UpdateAsync(user);
            return GetErrors(response);
        }
    }
}