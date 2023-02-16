using System;

namespace Demo.Common.Enums
{
    [Flags]
    public enum WeekOfMonthEnum
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last = 5
    }
}
