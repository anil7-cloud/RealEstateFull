using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Infrastructure.Persistence.Configurations
{
    public class PropertyMatchSalesAutomationAuditConfiguration
        : IEntityTypeConfiguration<PropertyMatchSalesAutomationAudit>
    {
        public void Configure(
            EntityTypeBuilder<PropertyMatchSalesAutomationAudit> builder)
        {
            builder.ToTable(
                "PropertyMatchSalesAutomationAudits");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AuditId)
                .IsRequired();

            builder.Property(x => x.EventType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Action)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Message)
                .HasMaxLength(2000);

            builder.Property(x => x.Actor)
                .HasMaxLength(200);

            builder.Property(x => x.Metadata)
                .HasMaxLength(4000);

            builder.Property(x => x.IpAddress)
                .HasMaxLength(100);

            builder.Property(x => x.CorrelationId)
                .HasMaxLength(200);

            builder.Property(x => x.Source)
                .HasMaxLength(200);

            builder.Property(x => x.ErrorCode)
                .HasMaxLength(100);

            builder.Property(x => x.ErrorMessage)
                .HasMaxLength(4000);

            builder.Property(x => x.Score)
                .HasPrecision(10, 2);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.AuditId)
                .IsUnique();

            builder.HasIndex(x => x.CreatedAt);

            builder.HasIndex(x => x.EventType);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.MatchId);

            builder.HasIndex(x => x.LeadId);

            builder.HasIndex(x => x.PropertyId);

            builder.HasIndex(x => x.ExecutionId);

            builder.HasIndex(x => x.Actor);

            builder.HasIndex(x => new
            {
                x.MatchId,
                x.CreatedAt
            });

            builder.HasIndex(x => new
            {
                x.LeadId,
                x.CreatedAt
            });

            builder.HasIndex(x => new
            {
                x.ExecutionId,
                x.CreatedAt
            });

            builder.HasIndex(x => new
            {
                x.EventType,
                x.Status,
                x.CreatedAt
            });
        }
    }
}
