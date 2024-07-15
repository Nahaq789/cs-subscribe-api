using Dapper;
using Npgsql;

namespace Subscribe.API.Application.Query.Subscribe;

public class SubscribeQueries(NpgsqlDataSource dataSource) : ISubscribeQueries
{
    public async Task<IEnumerable<SubscribeAggregateDto>> GetSubscribeByUserAsync(Guid userAggregateId)
    {
        await using var connection = await dataSource.OpenConnectionAsync();
        
        string sql = @"
            SELECT 
                s.subscribe_aggregate_id SubscribeAggregateId,
                s.payment_day PaymentDay,
                s.start_day StartDay,
                s.expected_date_of_cancellation ExpectedDateOfCancellation,
                s.color_code ColorCode,
                s.is_year IsYear,
                s.is_active IsActive,
                s.category_aggregate_id CategoryAggregeteId  ,
                s.user_aggregate_id UserAggregaeteId,
                s.delete_day DeleteDay,
                i.subscribe_name SubscribeName,
                i.amount Amount
            FROM subscribe_aggregate s
            INNER JOIN subscribe_item i ON s.subscribe_aggregate_id = i.subscribe_aggregate_id
            WHERE s.user_aggregate_id = @UserAggregateId;";
        
        var result = await connection.QueryAsync<SubscribeAggregateDto, SubscribeItemDto, SubscribeAggregateDto>(
            sql: sql,
            map: (subscribeAggregate, subscribeItem) =>
                {
                    subscribeAggregate.SubscribeItem = subscribeItem;
                    return subscribeAggregate;
                },
            param: new { UserAggregateId = userAggregateId },
            splitOn: "SubscribeName");
        
        return result;
    }

    public async Task<SubscribeAggregateDto> GetSubscribeByUserAndAggregateAsync(Guid subscribeAggregateId, Guid userAggregateId)
    {
        throw new NotImplementedException();
    }
}