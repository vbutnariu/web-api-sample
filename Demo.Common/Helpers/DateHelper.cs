using Demo.Common.Enums;
using System;

namespace Demo.Common.Helpers
{
    public class DateHelper
    {
        public static DateTime CalculateEasterSunday(int year)
        {
            int day = 0;
            int month = 0;
            int g = year % 19;
            int c = year / 100;
            int h = (c - (c / 4) - ((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));
            day = i - ((year + (year / 4) + i + 2 - c + (c / 4)) % 7) + 28;
            month = 3;
            if (day > 31)
            {
                month++;
                day -= 31;
            }
            return new DateTime(year, month, day);
        }

        public static DateTime Next(DateTime start, DayOfWeek day)
        {
            int offset = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(offset);
        }

        public static DateTime Previous(DateTime start, DayOfWeek day)
        {
            int offset = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(-7 + offset);
        }

        public static WeekDaysEnum ConvertDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return WeekDaysEnum.Monday;
                case DayOfWeek.Tuesday:
                    return WeekDaysEnum.Tuesday;
                case DayOfWeek.Wednesday:
                    return WeekDaysEnum.Wednesday;
                case DayOfWeek.Thursday:
                    return WeekDaysEnum.Thursday;
                case DayOfWeek.Friday:
                    return WeekDaysEnum.Friday;
                case DayOfWeek.Saturday:
                    return WeekDaysEnum.Saturday;
                case DayOfWeek.Sunday:
                    return WeekDaysEnum.Sunday;
                default:
                    return WeekDaysEnum.None;
            }
        }

        /// <summary>
        /// Determine if two date ranges overlap.
        /// </summary>
        /// <param name="startDate1"></param>
        /// <param name="startDate2"></param>
        /// <param name="endDate1"></param>
        /// <param name="endDate2"></param>
        /// <returns> Returns true if overlapping found, false otherwise.</returns>
        public static bool DateRangesOverlapping(DateTime? startDate1, DateTime? startDate2, DateTime? endDate1, DateTime? endDate2)
        {
            return ((startDate1 ?? DateTime.MinValue) <= (endDate2 ?? DateTime.MaxValue))
                        && ((startDate2 ?? DateTime.MinValue) <= (endDate1 ?? DateTime.MaxValue));
        }
    }
}
