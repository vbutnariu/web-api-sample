using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.EntityMetadata.Model
{
    public class FieldInfoModel
    {
        public FieldTypeEnum Type { get; set; }
        public object Value { get; set; }
    }
}
