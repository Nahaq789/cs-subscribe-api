namespace Subscribe.Domain.Model.SubscribeAggregate;

public interface ISubscribeRepository : IRepository<SubscribeAggregate>
{
    Task AddAsync(SubscribeAggregate aggregate);
    Task<SubscribeAggregate> FindBySubscribeAgIdAndUserAgId(Guid subscribeAgId, Guid UserAgId);
}