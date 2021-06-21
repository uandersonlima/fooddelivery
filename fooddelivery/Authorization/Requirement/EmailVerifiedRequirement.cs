using Microsoft.AspNetCore.Authorization;

namespace fooddelivery.Authorization.Requirement
{
    public class EmailVerifiedRequirement : IAuthorizationRequirement
    {
        public bool EmailVerified {get;}

        public EmailVerifiedRequirement(bool emailVerified)
        {
            EmailVerified = emailVerified;
        }
    }
}