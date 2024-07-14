namespace Subscribe.Domain.Model.SubscribeAggregate;

public interface ISubscribeRepository : IRepository<SubscribeAggregate>
{
    Task AddAsync(SubscribeAggregate aggregate);
    void UpdateAsync(SubscribeAggregate aggregate);
    //Task<SubscribeAggregate> FindBySubscribeAggregateId(Guid aggregateId);
}