using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using fooddelivery.Models.Users;

namespace fooddelivery.Service.Interfaces
{
    public interface IPermissionsService
    {
        Task AddClaimsToRoleAsync(Role role, List<Claim> claims);
        Task AddRolesToUserAsync(User user, List<Role> roles);
        Task<List<Role>>  GetAllRolesAsync();
        Task<List<Role>>  GetRolesByTypeAsync(string type);
        Task RemoveRoleClaimsAsync(Role role, List<Claim> claims);
        Task RemoveUserRolesAsync(User user, List<Role> roles);
    }
}