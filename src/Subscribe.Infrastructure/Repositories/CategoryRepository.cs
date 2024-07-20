using Subscribe.Domain.Model;
using Subscribe.Domain.SeedWork;
using Subscribe.Infrastructure.Context;

namespace Subscribe.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly SubscribeContext _subscribeContext;
    public IUnitOfWork UnitOfWork => _subscribeContext;

    public CategoryRepository(SubscribeContext subscribeContext)
    {
        _subscribeContext = subscribeContext;
    }

    public async Task AddAsync(CategoryAggregate categoryAggregate)
    {
        await _subscribeContext.CategoryAggregate.AddAsync(categoryAggregate);
    }

    public async Task<CategoryAggregate> FindByUserAndCategoryAggregateId(Guid categoryAggregateId, Guid userAggregateId)
    {
        throw new NotImplementedException();
    }

}
