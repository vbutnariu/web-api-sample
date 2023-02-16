using Demo.Common.EntityMetadata.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.EntityMetadata.Model
{
    public class MetadataModel 
    {
        [ModelResource("DosageMetadataModel.Id", 0, new int[] { 0 }, false)]
        public Guid Id { get; set; }
    }
}
