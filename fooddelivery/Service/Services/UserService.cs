using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;


namespace fooddelivery.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<StringBuilder> AddAsync(User user, string password)
        {
            return await _userRepository.AddAsync(user, password);
        }

        public async Task<StringBuilder> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await _userRepository.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userRepository.CheckPasswordAsync(user, password);
        }

        public async Task<StringBuilder> DeleteAsync(string id)
        {
            return await DeleteAsync(await GetUserByIdAsync(id));
        }

        public async Task<StringBuilder> DeleteAsync(User user)
        {
            return await _userRepository.DeleteAsync(user);
        }
        public async Task<List<User>> GetAllAsync(AppView appview)
        {
            return await _userRepository.GetAllAsync(appview);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userRepository.GeneratePasswordResetTokenAsync(user);
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<StringBuilder> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await _userRepository.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<StringBuilder> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
    }
}