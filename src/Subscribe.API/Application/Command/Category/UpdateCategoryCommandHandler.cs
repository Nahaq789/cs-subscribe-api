using MediatR;
using Subscribe.Domain.Model;

namespace Subscribe.API.Application.Command;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.FindByUserAndCategoryAggregateId(categoryAggregateId: command.AggregateId, userAggregateId: command.UserAggregateId);

        category.UpdateCategoryAggregate(
            colorCode: command.ColorCode,
            isActive: command.IsActive,
            iconFilePath: command.IconFilePath
        );

        category.UpdateCategoryItem(command.CategoryName, command.AggregateId);

        return await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}