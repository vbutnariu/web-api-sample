using System;

namespace Demo.Common.Calculation
{
    public class CalculationFactory
    {
        public static object GetScalarCalculator(string functionName)
        {
            switch (functionName)
            {
                case "EASTER":  return new EasterCalculator();
                default:
                    throw new ArgumentOutOfRangeException("functionName", "functionName not found!");
            }
        }
    }
}
