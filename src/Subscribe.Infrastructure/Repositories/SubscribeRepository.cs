using Microsoft.EntityFrameworkCore;
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

    public async Task<SubscribeAggregate> FindBySubscribeAgIdAndUserAgId(Guid subscribeAggregateId, Guid userAggregateId)
    {
        var subscribe = await _context.SubscribeAggregate
            .Include(e => e.SubscribeItem)
            .FirstOrDefaultAsync(p => p.SubscribeAggregateId == subscribeAggregateId && p._userAggregateId == userAggregateId);

        return subscribe;
    }
}