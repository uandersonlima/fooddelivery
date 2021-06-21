using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fooddelivery.Models.Users;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Service.Services
{
    public class PermissionsService : IPermissionsService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public PermissionsService(RoleManager<Role> roleManager,
        UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {

            _roleManager = roleManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task AddClaimsToRoleAsync(Role role, List<Claim> claims)
        {
            claims.ForEach(async claim => await _roleManager.AddClaimAsync(role, claim));
        }
        public async Task AddRolesToUserAsync(User user, List<Role> roles)
        {
            await _userManager.AddToRolesAsync(user, roles.Select(role => role.Name));
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<List<Role>> GetRolesByTypeAsync(string name)
        {
            var roles = await _roleManager.Roles.Where(role => role.Name == name).ToListAsync();
            return roles;
        }

        public async Task RemoveRoleClaimsAsync(Role role, List<Claim> claims)
        {
            claims.ForEach(async claim => await _roleManager.RemoveClaimAsync(role, claim));
        }

        public async Task RemoveUserRolesAsync(User user, List<Role> roles)
        {
            await _userManager.RemoveFromRolesAsync(user, roles.Select(role => role.Name));
        }
    }
}