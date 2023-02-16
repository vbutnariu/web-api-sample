using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.Core.DomainModel.QueryTypes;

namespace Demo.Core.Data.Mapping
{
    public partial class GuidQueryTypeMap : QueryTypeConfiguration<GuidQueryType>
    {
        public override void Configure(EntityTypeBuilder<GuidQueryType> builder)
        {
            builder.Property(x => x.Value).HasColumnNameCs("Value");
            base.Configure(builder);
        }
    }
    public partial class StringQueryTypeMap : QueryTypeConfiguration<StringQueryType>
    {
        public override void Configure(EntityTypeBuilder<StringQueryType> builder)
        {
            builder.Property(x => x.Value).HasColumnNameCs("Value");
            base.Configure(builder);
        }
    }
    public partial class IntegerQueryTypeMap : QueryTypeConfiguration<IntegerQueryType>
    {
        public override void Configure(EntityTypeBuilder<IntegerQueryType> builder)
        {
            builder.Property(x => x.Value).HasColumnNameCs("Value");
            base.Configure(builder);
        }
    }
   
    
    
    



}
