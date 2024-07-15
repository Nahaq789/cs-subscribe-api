namespace Subscribe.API.Application.Query.Subscribe;

public interface ISubscribeQueries
{
    Task<IEnumerable<SubscribeAggregateDto>> GetSubscribeByUserAsync(Guid userAggregateId);
    Task<SubscribeAggregateDto> GetSubscribeByUserAndAggregateAsync(Guid subscribeAggregateId, Guid userAggregateId);
}