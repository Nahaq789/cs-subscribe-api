
using Dapper;
using Npgsql;

namespace Subscribe.API.Application.Query.Category;

public class CategoryQueries(NpgsqlDataSource dataSource) : ICategoryQueries
{
    public async Task<CategoryAggregateDto> GetCategoryByUserAndAggregateAsync(Guid categoryAggregateId, Guid userAggregateId)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string sql = @"
            SELECT 
                c.category_aggregate_id CategoryAggregateId,
                c.color_code ColorCode,
                c.icon_file_path IconFilePath,
                c.is_default IsDefault,
                c.is_active IsActive,
                i.category_name CategoryName
            FROM category_aggregate c
            INNER JOIN category_item i ON c.category_aggregate_id = i.category_aggregate_id
            WHERE c.category_aggregate_id = @CategoryAggregateId AND c.user_aggregate_id = @UserAggregateId";

        var result = await connection.QueryFirstOrDefaultAsync<CategoryAggregateDtoWithItem>(
            sql: sql,
            param: new { CategoryAggregateId = categoryAggregateId, UserAggregateId = userAggregateId });

        if (result == null)
        {
            return null;
        }

        var categoryDto = new CategoryAggregateDto
        {
            CategoryAggregateId = result.CategoryAggregateId,
            ColorCode = result.ColorCode,
            IconFilePath = result.IconFilePath,
            IsDefault = result.IsDefault,
            IsActive = result.IsActive,
            UserAggregateId = result.UserAggregateId,
            CategoryItem = new CategoryItemDto
            {
                CategoryName = result.CategoryName
            }
        };

        return categoryDto;
    }

    public async Task<IEnumerable<CategoryAggregateDto>> GetCategoryAggregateListAsync(Guid userAggregateId)
    {
        throw new NotImplementedException();
    }
}

internal class CategoryAggregateDtoWithItem
{
    public Guid CategoryAggregateId { get; set; }
    public string ColorCode { get; set; }
    public string IconFilePath { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public Guid UserAggregateId { get; set; }
    public string CategoryName { get; set; }
}