using Microsoft.EntityFrameworkCore;
using Subscribe.Domain.Model;

public class SubscribeContext : DbContext
{
    public DbSet<SubscribeAggregate> SubscribeAggregate { get; set; }
    public DbSet<SubscribeItem> SubscribeItem { get; set; }
    public DbSet<CategoryAggregate> CategoryAggregate { get; set; }
    public DbSet<CategoryItem> CategoryItem { get; set; }

    public SubscribeContext(DbContextOptions<SubscribeContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SubscribeAggregateConfiguration());
        modelBuilder.ApplyConfiguration(new SubscribeItemConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryAggregateConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryItemConfiguration());
    }
}