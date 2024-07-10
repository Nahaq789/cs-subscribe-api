using Microsoft.EntityFrameworkCore;
using Subscribe.Domain.Model;

internal class CategoryItemConfiguration : IEntityTypeConfiguration<CategoryItem>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CategoryItem> builder)
    {
        builder.ToTable("category_item");
        builder.HasKey(p => p.CategoryItemId);
        builder.Property(p => p.CategoryItemId)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasColumnName("category_item_id");

        builder.Property(e => e.CategoryName)
            .IsRequired()
            .HasColumnName("category_name");

        builder.Property(e => e.CategoryAggregateId)
            .IsRequired()
            .HasColumnName("category_aggregate_id");
        builder.HasOne<CategoryAggregate>()
            .WithOne()
            .HasForeignKey<CategoryItem>(f => f.CategoryAggregateId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(p => p.CategoryItemId);
        builder.HasIndex(f => f.CategoryAggregateId);
    }
}
