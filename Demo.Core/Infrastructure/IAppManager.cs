using System;

namespace Demo.Core.Infrastructure
{
    public interface IAppManager
    {            
        void RunModule(Type type, bool createNewInstance = false);
        IModule GetModuleByName(string moduleName);

    }
}
