using Demo.Common.Constant;

namespace Demo.Core.Infrastructure
{
    public class DefaultAdminIdentityProvider : IAuthorizationIdentityProvider
    {
        
        UserIdentity IAuthorizationIdentityProvider.GetCurrentUserIdentity()
        {
            return new UserIdentity()
            {
                UserId = ApplicationConstants.GLOBAL_ADMIN,
                UserName = "NCAdmin",
                FirstName = "Global",
                LastName = "Admin"
            };
        }
    }
}
