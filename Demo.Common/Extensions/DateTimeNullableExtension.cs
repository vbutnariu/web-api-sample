using System;

namespace Demo.Common.Extensions
{
    public static class DateTimeNullableExtension
    {
        public static string ToString(this DateTime? source, string dateFormat)
        {
            return source == null ? string.Empty : ((DateTime)source).ToString(dateFormat);
        }
        
        public static string ToString(this DateTime? source, string dateFormat, System.Globalization.CultureInfo currentCulture)
        {
            return source == null ? string.Empty : ((DateTime)source).ToString(dateFormat, currentCulture);
        }
    }
}