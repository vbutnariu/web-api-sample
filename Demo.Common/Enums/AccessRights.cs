using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Enums
{
    /// <summary>
    /// To be replaced with service-side enum
    /// </summary>
    [Flags]
    public enum AccessRights
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4,
        Delete = 8,
        ReadWrite = Read | Write,
        All = Read | Write | Execute | Delete
    }
}
