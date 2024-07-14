using Subscribe.Domain.SeedWork;

namespace Subscribe.Infrastructure.Repositories;

public class SubscribeRepository : ISubscribeRepository
{
    private readonly SubscribeContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public SubscribeRepository(SubscribeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync(SubscribeAggregate aggregate)
    {
        await _context.SubscribeAggregate.AddAsync(aggregate);
    }
}