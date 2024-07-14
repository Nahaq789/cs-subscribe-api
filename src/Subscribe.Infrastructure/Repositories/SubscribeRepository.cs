using Subscribe.Domain.Model.SubscribeAggregate;
using Subscribe.Domain.SeedWork;
using Subscribe.Infrastructure.Context;

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

    public void UpdateAsync(SubscribeAggregate aggregate)
    {
        _context.SubscribeAggregate.Update(aggregate);
    }
}