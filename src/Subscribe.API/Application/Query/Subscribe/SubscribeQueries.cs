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
                s.category_aggregate_id CategoryAggregateId,
                s.user_aggregate_id UserAggregateId,
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
                s.category_aggregate_id CategoryAggregateId,
                s.user_aggregate_id UserAggregateId,
                s.delete_day DeleteDay,
                i.subscribe_name SubscribeName,
                i.amount Amount
            FROM subscribe_aggregate s
            INNER JOIN subscribe_item i ON s.subscribe_aggregate_id = i.subscribe_aggregate_id
            WHERE s.subscribe_aggregate_id = @SubscribeAggregateId AND s.user_aggregate_id = @UserAggregateId;";

        var result = await connection.QueryFirstOrDefaultAsync<SubscribeAggregateDtoWithItem>(
            sql: sql,
            param: new { SubscribeAggregateId = subscribeAggregateId, UserAggregateId = userAggregateId });

        if (result == null)
        {
            return null;
        }

        var aggregateDto = new SubscribeAggregateDto
        {
            SubscribeAggregateId = result.SubscribeAggregateId,
            PaymentDay = result.PaymentDay,
            StartDay = result.StartDay,
            ExpectedDateOfCancellation = result.ExpectedDateOfCancellation,
            ColorCode = result.ColorCode,
            IsYear = result.IsYear,
            IsActive = result.IsActive,
            CategoryAggregateId = result.CategoryAggregateId,
            UserAggregateId = result.UserAggregateId,
            DeleteDay = result.DeleteDay,
            SubscribeItem = new SubscribeItemDto
            {
                SubscribeName = result.SubscribeName,
                Amount = result.Amount
            }
        };
        return aggregateDto;
    }
}

internal class SubscribeAggregateDtoWithItem
{
    public Guid SubscribeAggregateId { get; set; }
    public DateTime PaymentDay { get; set; }
    public DateTime StartDay { get; set; }
    public DateTime? ExpectedDateOfCancellation { get; set; }
    public string ColorCode { get; set; }
    public bool IsYear { get; set; }
    public bool IsActive  { get; set; }
    public Guid CategoryAggregateId { get; set; }
    public Guid UserAggregateId { get; set; }
    public DateTime? DeleteDay { get; set; }
    public string SubscribeName { get; set; }
    public decimal Amount { get; set; }
}