using Demo.Common.Enums;
using System;

namespace Demo.Common.Extensions
{
    public static class WeekDaysEnumExtensions
	{
        public static DayOfWeek ToDayOfWeek(this WeekDaysEnum weekDays)
        {
            return weekDays switch
            {
                WeekDaysEnum.Monday => DayOfWeek.Monday,
                WeekDaysEnum.Tuesday => DayOfWeek.Tuesday,
                WeekDaysEnum.Wednesday => DayOfWeek.Wednesday,
                WeekDaysEnum.Thursday => DayOfWeek.Thursday,
                WeekDaysEnum.Friday => DayOfWeek.Friday,
                WeekDaysEnum.Saturday => DayOfWeek.Saturday,
                WeekDaysEnum.Sunday => DayOfWeek.Sunday,
                _ => DayOfWeek.Monday,
            };
        }
    }
}
