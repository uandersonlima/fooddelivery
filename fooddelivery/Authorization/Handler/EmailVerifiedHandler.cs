using System.Security.Claims;
using System.Threading.Tasks;
using fooddelivery.Authorization.Requirement;
using fooddelivery.Models.Constants;
using Microsoft.AspNetCore.Authorization;

namespace fooddelivery.Authorization.Handler
{
    public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "EmailVerified"))
            {
                return Task.CompletedTask;
            }

            var emailVerified = bool.Parse(context.User.FindFirst(claim => claim.Type == Policy.EmailVerified).Value); 

            if (emailVerified == requirement.EmailVerified)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}