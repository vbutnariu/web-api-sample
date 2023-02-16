using System;

namespace Demo.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class HideInLookupsAttribute : Attribute
    {
        public HideInLookupsAttribute()
        {

        }
    }

      
}
