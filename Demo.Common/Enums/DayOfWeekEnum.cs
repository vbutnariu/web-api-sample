using System;
using System.ComponentModel;

namespace Demo.Common.Enums
{
    [Flags]
    public enum DayOfWeekEnum
    {
        [Description("None")]
        None = 0,
        [Description("Sunday")]
        Sunday = 1,
        [Description("Monday")]
        Monday = 2,
        [Description("Tuesday")]
        Tuesday = 3,
        [Description("Wednesday")]
        Wednesday = 4,
        [Description("Thursday")]
        Thursday = 5,
        [Description("Friday")]
        Friday = 6,
        [Description("Saturday")]
        Saturday = 7
    }
}
