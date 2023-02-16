using Demo.Common.Helpers;
using System;

namespace Demo.Common.Calculation
{
    public class EasterCalculator : IScalarCalculator<DateTime, DateTime>
    {
        public DateTime Calculate(DateTime value)
        {
            return DateHelper.CalculateEasterSunday(value.Year);
        }
    }


    
}
