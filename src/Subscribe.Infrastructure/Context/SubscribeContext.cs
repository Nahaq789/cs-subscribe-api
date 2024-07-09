using Microsoft.EntityFrameworkCore;

public class SubscribeContext : DbContext
{
    public DbSet<SubscribeAggregate> SubscribeAggregates { get; set; }
    public DbSet<SubscribeItem> SubscribeItems { get; set; }

    public SubscribeContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SubscribeAggregateConfiguration());
        modelBuilder.ApplyConfiguration(new SubscribeItemConfiguration());
    }
}