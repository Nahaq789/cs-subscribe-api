public interface ISubscribeRepository : IRepository<SubscribeAggregate>
{
    Task<SubscribeAggregate> GetSubscribeFindByAggregateIdAsync(Guid aggregateId);
}