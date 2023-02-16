using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Infrastructure
{
    /// <summary>
    /// Application information class
    /// </summary>
    public interface IApplicationInfo
    {
        public string Name { get; }
        public string Path { get; }
        public string Environment { get; }

    }
}
