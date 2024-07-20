using MediatR;
using Subscribe.Domain.Model;

namespace Subscribe.API.Application.Command;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new CategoryAggregate(categoryAggregateId: Guid.NewGuid(), colorCode: command.ColorCode, iconFilePath: command.IconFilePath, isActive: true, categoryName: command.CategoryName, userAggregateId: command.UserAggregateId);

        await _categoryRepository.AddAsync(category);
        return await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
