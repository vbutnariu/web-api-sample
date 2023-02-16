using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.EntityMetadata.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ModelResourceAttribute : Attribute
    {
        public ModelResourceAttribute(string resourceKey, int index, int[] fields) 
            : this(resourceKey, index, fields, true)
        {

        }

        public ModelResourceAttribute(string resourceKey, int index, int[] fields, bool visible)
        {
            this.ResourceKey = resourceKey;
            this.Index = index;
            this.Fields = fields;
            this.Visible = visible;
        }

        public string Name { get; set; }
        public string ResourceKey { get; set; }
        public int Index { get; set; }
        public int[] Fields { get; set; }
        public bool Visible { get; set; }
    }
}
