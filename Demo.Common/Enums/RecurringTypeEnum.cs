using System;

namespace Demo.Common.Enums
{
    [Flags]
    public enum RecurringTypeEnum
    {
        None = 0,
        Once = 1,
        Daily = 2,
        Weekly = 4,
        Monthly = 8,
        Yearly = 16,
    }
}
