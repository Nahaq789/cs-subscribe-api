using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SubscribeItemConfiguration : IEntityTypeConfiguration<SubscribeItem>
{
    public void Configure(EntityTypeBuilder<SubscribeItem> builder)
    {
        builder.ToTable("subscribe_item");
        builder.HasKey(x => x.SubscribeItemId);
        builder.Property(x => x.SubscribeItemId)
            .ValueGeneratedOnAdd()
            .HasColumnName("subscribe_item_id");

        builder.Property(e => e.SubscribeName)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("subscribe_name");

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasColumnName("amount");

        builder.Property(e => e.SubscribeAggregateId)
            .IsRequired()
            .HasColumnName("subscribe_aggregate_id");
        builder.HasOne<SubscribeAggregate>()
            .WithOne()
            .HasForeignKey<SubscribeItem>(x => x.SubscribeAggregateId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => x.SubscribeItemId);
        builder.HasIndex(f => f.SubscribeAggregateId);
    }
}