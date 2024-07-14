using Microsoft.EntityFrameworkCore;
using Subscribe.Domain.Model;
using MediatR;
using Subscribe.Domain.SeedWork;

public class SubscribeContext : DbContext, IUnitOfWork
{
    public DbSet<SubscribeAggregate> SubscribeAggregate { get; set; }
    public DbSet<SubscribeItem> SubscribeItem { get; set; }
    public DbSet<CategoryAggregate> CategoryAggregate { get; set; }
    public DbSet<CategoryItem> CategoryItem { get; set; }

    private readonly IMediator _mediator;

#pragma warning disable CS8618 
    public SubscribeContext(DbContextOptions<SubscribeContext> options) : base(options) { }
#pragma warning restore CS8618
    public SubscribeContext(DbContextOptions<SubscribeContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SubscribeAggregateConfiguration());
        modelBuilder.ApplyConfiguration(new SubscribeItemConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryAggregateConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryItemConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}

#nullable enable