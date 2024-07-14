public interface ISubscribeRepository : IRepository<SubscribeAggregate>
{
    Task AddAsync(SubscribeAggregate aggregate);
}