using System;
using System.Collections.Generic;

namespace Demo.Common.Extensions
{
    public static class DateExtensions
    {
        public static DateTime MonthFirstDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime StartOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1);
        }

        public static DateTime EndOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 12, 31);
        }

        public static DateTime MonthEndDay(this DateTime value)
        {
            var firstDate = value.MonthFirstDay();
            return firstDate.AddMonths(1).AddSeconds(-1);
        }

        public static DateTime StartOfWeek(this DateTime value, DayOfWeek firstDayOfWeek)
        {
            int diff = (7 + (value.DayOfWeek - firstDayOfWeek)) % 7;
            return value.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfWeekInMonth(this DateTime value, DayOfWeek firstDayOfWeek)
        {
            int diff = (7 + (value.DayOfWeek - firstDayOfWeek)) % 7;
            if (value.Month != value.AddDays(-1 * diff).Date.Month)
            {
                return new DateTime(value.Year, value.Month, 1);
            }
            return value.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Get the DateTime in Central European Time time zone
        /// </summary>
        /// <returns></returns>
        public static DateTime ToCET(this DateTime value)
        {
            var date = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
            return DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        }

        public static DateTime EndOfWeek(this DateTime dt)
        {
            return dt.StartOfWeek(DayOfWeek.Monday).AddDays(6);
        }

        public static DateTime StartOfDay(this DateTime dt)
        {
            return dt.Date;
        }

        public static DateTime EndOfDay(this DateTime dt)
        {
            return dt.Date.AddDays(1).AddTicks(-1);
        }

        public static string ToJsonDateTime(this DateTime value)
        {
            return value.ToString("o");
        }



        /// <summary>
        /// transform date as UTC by cutting time zone
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJsonDateUtc(this DateTime value)
        {
            return DateTime.SpecifyKind(value.Date, DateTimeKind.Utc).ToString("o");
        }

        /// <summary>
        /// transform date as UTC by cutting time zone
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJsonDateTimeUtc(this DateTime value)
        {
            return DateTime.SpecifyKind(value, DateTimeKind.Utc).ToString("o");
        }

        /// <summary>
        /// transform date as UTC by cutting time zone
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJsonDateTimeUtcWithTimeZone(this DateTime value)
        {
            return value.ToUniversalTime().ToString("o");
        }


    }
}
