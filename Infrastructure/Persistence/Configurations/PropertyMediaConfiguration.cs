using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PropertyMediaConfiguration : IEntityTypeConfiguration<PropertyMedia>
{
    public void Configure(EntityTypeBuilder<PropertyMedia> builder)
    {
        builder.ToTable("PropertyMedia");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MediaUrl)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.ThumbnailUrl)
            .HasMaxLength(2000);

        builder.Property(x => x.MediaType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Title)
            .HasMaxLength(250);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.Property(x => x.IsPrimary)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.PropertyId,
            x.DisplayOrder
        });

        builder.HasIndex(x => new
        {
            x.PropertyId,
            x.IsPrimary
        });

        builder.HasOne(x => x.Property)
            .WithMany()
            .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
