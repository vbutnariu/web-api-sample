namespace Demo.Core.Infrastructure
{
    public interface IAuthorizationIdentityProvider
    {
        UserIdentity GetCurrentUserIdentity();
    }
}