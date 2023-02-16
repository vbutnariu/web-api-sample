
namespace Demo.Core.Infrastructure
{
    public interface IModule
    {
        string ModuleName { get; }

        void Run();
    }
}