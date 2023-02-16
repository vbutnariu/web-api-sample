namespace Demo.Services.Installation
{
    public interface IInstallService
    {
        string GetClientVersion();
        string PrepareZip(string destinationFolder, bool forceCreate);
    }
}