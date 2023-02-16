namespace Demo.Common.Configuration
{
    public interface IClientApplicationConfiguration

    {
        string ServerAddress { get; }
        string ClientId { get; }
    }
}
