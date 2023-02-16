using Demo.Core.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.Core.DomainModel.Authorization;

namespace Demo.Core.Data.Mapping
{

    public class RestClientMap : EntityTypeConfiguration<RestClient>
    {

        public override void Configure(EntityTypeBuilder<RestClient> builder)
        {

            // Primary Key
            builder.HasKey(t => t.Id);
            
            // Properties
            builder.Property(t => t.Secret)
                .IsRequired();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);            

            // Table & Column Mappings
            builder.ToTableCs("RestClient");
            builder.Property(t => t.Id).HasColumnNameCs("RestClientId");
            builder.Property(t => t.Secret).HasColumnNameCs("Secret");
            builder.Property(t => t.Name).HasColumnNameCs("Name");
            builder.Property(t => t.Active).HasColumnNameCs("Active");
            builder.Property(t => t.ApplicationType).HasColumnNameCs("ApplicationType");
            builder.Property(t => t.RefreshTokenLifeTime).HasColumnNameCs("RefreshTokenLifeTime");
            builder.Property(t => t.AllowedOrigin).HasColumnNameCs("AllowedOrigin");
            base.Configure(builder);
        }

        public RestClientMap()
        {
       


        }

        protected override void PostConfigureSqlServer(EntityTypeBuilder<RestClient> builder)
        {
            builder.Property(t => t.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            builder.Property(t => t.AllowedOrigin)
                .IsRequired()
                .HasMaxLength(250)
                .HasDefaultValueSql("(N'*')");
            builder.Property(e => e.RefreshTokenLifeTime).HasDefaultValueSql("((9999))");
        }

        protected override void PostConfigurePostgres(EntityTypeBuilder<RestClient> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(md5(((random())::text || (clock_timestamp())::text)))::uuid");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true");
            builder.Property(t => t.AllowedOrigin)
                .IsRequired()
                .HasMaxLength(250)
                .HasDefaultValueSql("'*'::character varying");
            builder.Property(e => e.RefreshTokenLifeTime).HasDefaultValueSql("9999");
        }
    }
}
