using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.Core.DomainModel.Authorization;
using Demo.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Data.Mapping
{
    public class RefreshTokenMap : EntityTypeConfiguration<RefreshToken>
    {

        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.ProtectedTicket)
                .IsRequired();

            builder.Property(t => t.RemoteIPAddress)
               .HasMaxLength(100);

            // Table & Column Mappings
            builder.ToTableCs("RefreshToken");
            builder.Property(t => t.Id).HasColumnNameCs("RefreshTokenId");
            builder.Property(t => t.ProtectedTicket).HasColumnNameCs("ProtectedTicket");
            builder.Property(t => t.IssuedUtc).HasColumnNameCs("IssuedUtc");
            builder.Property(t => t.RestClientId).HasColumnNameCs("RestClientId");
            builder.Property(t => t.ExpiresUtc).HasColumnNameCs("ExpiresUtc");
            builder.Property(t => t.RemoteIPAddress).HasColumnNameCs("RemoteIPAddress");

            // Relationships
           

            builder.HasOne(t => t.RestClient)
                .WithMany(t => t.RefreshTokens)
                .HasForeignKey(d => d.RestClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_RestClient");

            base.Configure(builder);
        }
        public RefreshTokenMap()
        {


        }

        protected override void PostConfigureSqlServer(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(t => t.Id).HasDefaultValueSql("(newid())");
        }

        protected override void PostConfigurePostgres(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(md5(((random())::text || (clock_timestamp())::text)))::uuid");
        }
    }
}
