using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

    }
}