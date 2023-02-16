using Demo.Common.Constant;
using Demo.Common.Enums;
using Demo.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string ToLocalizedString(this Enum value)
        {
            return LocalizationService.Localize($"{value.GetType().Name}.{value.ToString()}");
        }

       

        public static List<string> ToLocalizedStringList<T>(this T enumValue) where T : Enum
        {
            var stringValues = new List<string>();
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
            foreach (var value in enumValues)
            {
                stringValues.Add(value.ToLocalizedString());
            }
            return stringValues;
        }

        public static List<(T, string)> ToLocalizedValueStringList<T>(this Type enumValue) where T : Enum
        {
            var stringValues = new List<(T, string)>();
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
            foreach (var value in enumValues)
            {
                stringValues.Add((value, value.ToLocalizedString()));
            }
            return stringValues;
        }

        public static bool TryGetEnumValue<T>(this string value, out T result) where T : struct
        {

            if (Enum.TryParse<T>(value, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static string TryGetEnumTranslation<T>(this string enumValue, string defaultValue) where T : struct
        {
            T result = default(T);
            if (typeof(T).IsEnum && enumValue.TryGetEnumValue(out result))
            {
                Enum value = result as Enum;
                return value.ToLocalizedString();
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
