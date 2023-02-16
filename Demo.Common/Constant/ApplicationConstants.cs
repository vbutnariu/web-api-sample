using System;

namespace Demo.Common.Constant
{
    public static class ApplicationConstants
    {
      
        public static readonly Guid GLOBAL_ADMIN;
      
        static ApplicationConstants()
        {
           
            GLOBAL_ADMIN = new Guid(GLOBAL_ADMIN_GUID);
           
        }

        public const int DIALOG_HEIGHT = 185;
        public const int DIALOG_WIDTH = 480;

        public const string DATA_CONNECTIONSTRING_KEY = "DbContext";
        public const string DATA_CONNECTIONSTRING_KEY_POSTGRES = "DbContextPostgres";
        public const int MAX_POSTALCODE = 5;
        private const string GLOBAL_ADMIN_GUID = "99999999-9999-9999-9999-999999999999";
        public const string DEFAULT_COUNTRY = "DE";

        
    }
}
