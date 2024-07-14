using Microsoft.EntityFrameworkCore;
using Subscribe.Domain.Model;
using Subscribe.Domain.SeedWork;
using Subscribe.Domain.Model.SubscribeAggregate;
using Subscribe.Infrastructure.EntityConfiguration;

namespace Subscribe.Infrastructure.Context;

public class SubscribeContext : DbContext, IUnitOfWork
{
    public DbSet<CategoryAggregate> CategoryAggregate { get; set; }
    public DbSet<CategoryItem> CategoryItem { get; set; }
    public DbSet<SubscribeAggregate> SubscribeAggregate { get; set; }
    public DbSet<SubscribeItem> SubscribeItem { get; set; }

    public SubscribeContext(DbContextOptions<SubscribeContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SubscribeItemConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryItemConfiguration());
        modelBuilder.ApplyConfiguration(new SubscribeAggregateConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryAggregateConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        _ = await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}

#nullable enable