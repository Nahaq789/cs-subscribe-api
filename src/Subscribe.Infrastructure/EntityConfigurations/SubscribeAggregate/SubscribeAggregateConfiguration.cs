using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscribe.Domain.Model;
using Subscribe.Domain.Model.SubscribeAggregate;

namespace Subscribe.Infrastructure.EntityConfiguration;

internal class SubscribeAggregateConfiguration : IEntityTypeConfiguration<SubscribeAggregate>
{
    public void Configure(EntityTypeBuilder<SubscribeAggregate> builder)
    {
        builder.ToTable("subscribe_aggregate");
        builder.HasKey(p => p.SubscribeAggregateId);
        builder.Property(e => e.SubscribeAggregateId)
            .HasColumnName("subscribe_aggregate_id");

        builder.Property(e => e.PaymentDay)
            .IsRequired()
            .HasColumnName("payment_day");

        builder.Property(e => e.StartDay)
            .IsRequired()
            .HasColumnName("start_day");

        builder.Property(e => e.ExpectedDateOfCancellation)
            .HasColumnName("expected_date_of_cancellation");

        builder.Property(e => e.ColorCode)
            .IsRequired()
            .HasMaxLength(7)
            .HasColumnName("color_code");

        builder.Property(e => e.IsYear)
            .IsRequired()
            .HasColumnName("is_year");

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(e => e.StartDay)
            .IsRequired()
            .HasColumnName("start_day");

        builder.Property(e => e._userAggregateId)
            .IsRequired()
            .HasColumnName("user_aggregate_id");

        builder.Property(e => e.DeleteDay)
            .HasColumnName("delete_day");

        builder.HasOne(e => e.SubscribeItem)
            .WithOne()
            .HasForeignKey<SubscribeItem>(f => f.SubscribeAggregateId);

        builder.Property(e => e._categoryAggregateId)
            .IsRequired()
            .HasColumnName("category_aggregate_id");

        builder.HasOne<CategoryAggregate>()
            .WithMany()
            .HasForeignKey(f => f._categoryAggregateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(f => f._categoryAggregateId);
    }
}