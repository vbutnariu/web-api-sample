using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.Model.Helpers
{
    public class DateHelper
    {
        public static Day ToDayEnum(WeekDaysEnum dayOfWeek)
        {
            Day days = 0;
            if ((dayOfWeek & WeekDaysEnum.Monday) == WeekDaysEnum.Monday)
            {
                days |= Day.MONDAY;
            }
            if ((dayOfWeek & WeekDaysEnum.Tuesday) == WeekDaysEnum.Tuesday)
            {
                days |= Day.TUESDAY;
            }
            if ((dayOfWeek & WeekDaysEnum.Wednesday) == WeekDaysEnum.Wednesday)
            {
                days |= Day.WEDNESDAY;
            }
            if ((dayOfWeek & WeekDaysEnum.Thursday) == WeekDaysEnum.Thursday)
            {
                days |= Day.THURSDAY;
            }
            if ((dayOfWeek & WeekDaysEnum.Friday) == WeekDaysEnum.Friday)
            {
                days |= Day.FRIDAY;
            }
            if ((dayOfWeek & WeekDaysEnum.Saturday) == WeekDaysEnum.Saturday)
            {
                days |= Day.SATURDAY;
            }
            if ((dayOfWeek & WeekDaysEnum.Sunday) == WeekDaysEnum.Sunday)
            {
                days |= Day.SUNDAY;
            }

            return days;
        }        
    }
}
