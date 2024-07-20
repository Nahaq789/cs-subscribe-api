namespace Subscribe.Domain.Model;

public interface ICategoryRepository : IRepository<CategoryAggregate>
{
    Task AddAsync(CategoryAggregate categoryAggregate);
    Task<CategoryAggregate> FindByUserAndCategoryAggregateId(Guid categoryAggregateId, Guid userAggregateId);
}