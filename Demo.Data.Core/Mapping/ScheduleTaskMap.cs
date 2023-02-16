using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.Core.DomainModel;

namespace Demo.Data.Core.Mapping
{
	public class ScheduleTaskMap : EntityTypeConfiguration<ScheduleTask>
    {
        public override void Configure(EntityTypeBuilder<ScheduleTask> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(500);
            // Table & Column Mappings
            builder.ToTableCs("ScheduleTask");
            builder.Property(t => t.Id).HasColumnNameCs("ScheduleTaskId");
            builder.Property(t => t.Name).HasColumnNameCs("Name");
            builder.Property(t => t.Seconds).HasColumnNameCs("Seconds");
            builder.Property(t => t.Type).HasColumnNameCs("Type");
            builder.Property(t => t.Enabled).HasColumnNameCs("Enabled");
            builder.Property(t => t.StopOnError).HasColumnNameCs("StopOnError");
            builder.Property(t => t.LastStartUtc).HasColumnNameCs("LastStartUtc");
            builder.Property(t => t.LastEndUtc).HasColumnNameCs("LastEndUtc");
            builder.Property(t => t.LastSuccessUtc).HasColumnNameCs("LastSuccessUtc");
           

            // Indexes

            // Relationships

            base.Configure(builder);
        }

        protected override void PostConfigureSqlServer(EntityTypeBuilder<ScheduleTask> builder)
        {
            builder.Property(t => t.Id).HasDefaultValueSql("(newid())");
        }

        protected override void PostConfigurePostgres(EntityTypeBuilder<ScheduleTask> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(md5(((random())::text || (clock_timestamp())::text)))::uuid");
        }
    }
}