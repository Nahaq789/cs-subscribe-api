using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscribe.Domain.Model;

namespace Subscribe.Infrastructure.EntityConfiguration;

internal class CategoryAggregateConfiguration : IEntityTypeConfiguration<CategoryAggregate>
{
    public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
    {
        builder.ToTable("category_aggregate");
        builder.HasKey(p => p.CategoryAggregateId);
        builder.Property(p => p.CategoryAggregateId)
            .IsRequired()
            .HasColumnName("category_aggregate_id");

        builder.Property(e => e.ColorCode)
            .IsRequired()
            .HasMaxLength(7)
            .HasColumnName("color_code");

        builder.Property(e => e.IconFilePath)
            .HasColumnName("icon_file_path");

        builder.Property(e => e.IsDefault)
            .IsRequired()
            .HasColumnName("is_default");

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.HasOne(e => e.CategoryItem)
            .WithOne()
            .HasForeignKey<CategoryItem>(f => f.CategoryAggregateId);
    }
}