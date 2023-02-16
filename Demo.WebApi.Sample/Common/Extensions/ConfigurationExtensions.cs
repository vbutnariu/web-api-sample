using Demo.Common.Constant;
using Microsoft.Extensions.Configuration;
using System;

namespace Demo.WebApi.Ergodat.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static int TokenTimeout(this IConfiguration value)
        {
            //int tokenTimeoutSeconds;
            //int.TryParse(value["TokenTimeoutSeconds"], out tokenTimeoutSeconds);
            //if (tokenTimeoutSeconds == 0)
            //{
            //    tokenTimeoutSeconds = 6000;    //set default to 5 if not present in the config
            //}
            //return tokenTimeoutSeconds;

            return GetValue<int>(value, "TokenTimeoutSeconds", 6000);
        }

        public static string ConnectionString(this IConfiguration value)
        {

            if (value.GetValue<bool>("UsePostgres"))
            {
                return value.GetConnectionString(ApplicationConstants.DATA_CONNECTIONSTRING_KEY_POSTGRES);
            }
            else
            {
                return value.GetConnectionString(ApplicationConstants.DATA_CONNECTIONSTRING_KEY);
            }
        }

        public static bool AuthenticationEnabled(this IConfiguration value)
        {
            return GetValue<bool>(value, "Authenticate", false);
        }

        public static T GetValue<T>(this IConfiguration value, string key, T defaultValue)
        {
            try
            {
                return (T)Convert.ChangeType(value[key], typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}