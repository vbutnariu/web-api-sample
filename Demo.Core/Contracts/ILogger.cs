using System;

namespace Demo.Core.Services
{
    public interface ILogger
    {
        void WriteError(string message);
        void WriteError(string message, Exception ex);  
        void WriteWarning(string message);
        void WriteInfo(string message);
    }
}
