namespace Subscribe.API.Application.Query.Category;
public interface ICategoryQueries
{
    Task<CategoryAggregateDto> GetCategoryByUserAndAggregateAsync(Guid categoryAggregateId, Guid userAggregateId);
    Task<IEnumerable<CategoryAggregateDto>> GetCategoryAggregateListAsync(Guid userAggregateId);
}
