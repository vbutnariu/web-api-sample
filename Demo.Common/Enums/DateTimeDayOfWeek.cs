using System;

namespace Demo.Common.Enums
{
    [Flags]
    public enum DateTimeDayOfWeek
    {
        //
        // Summary:
        //     Indicates Sunday.
        Sunday = 1,
        //
        // Summary:
        //     Indicates Monday.
        Monday = 2,
        //
        // Summary:
        //     Indicates Tuesday.
        Tuesday = 4,
        //
        // Summary:
        //     Indicates Wednesday.
        Wednesday = 8,
        //
        // Summary:
        //     Indicates Thursday.
        Thursday = 16,
        //
        // Summary:
        //     Indicates Friday.
        Friday = 32,
        //
        // Summary:
        //     Indicates Saturday.
        Saturday = 64
    }

    public static class DateTimeDayOfWeekUtils
    {
        public static DateTimeDayOfWeek GetAllWeekDays()
        {
            return DateTimeDayOfWeek.Sunday | DateTimeDayOfWeek.Monday | DateTimeDayOfWeek.Tuesday | DateTimeDayOfWeek.Wednesday
                | DateTimeDayOfWeek.Thursday | DateTimeDayOfWeek.Friday | DateTimeDayOfWeek.Saturday;
        }
        public static DateTimeDayOfWeek GetDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return DateTimeDayOfWeek.Monday;
                case DayOfWeek.Tuesday:
                    return DateTimeDayOfWeek.Tuesday;
                case DayOfWeek.Wednesday:
                    return DateTimeDayOfWeek.Wednesday;
                case DayOfWeek.Thursday:
                    return DateTimeDayOfWeek.Thursday;
                case DayOfWeek.Friday:
                    return DateTimeDayOfWeek.Friday;
                case DayOfWeek.Saturday:
                    return DateTimeDayOfWeek.Saturday;
                case DayOfWeek.Sunday:
                    return DateTimeDayOfWeek.Sunday;
                default:
                    break;
            }
            return 0;
        }
    }
}
