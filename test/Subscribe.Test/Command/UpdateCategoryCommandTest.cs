using Moq;
using Subscribe.API.Application.Command;
using Subscribe.Domain.Model;
using Subscribe.Domain.SeedWork;
using Xunit;

namespace Subscribe.Test.Command;

/// <summary>
/// CategoryCommand のテストを提供します。
/// </summary>
public class UpdateCategoryCommandTest
{
    private readonly Mock<ICategoryRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateCategoryCommandHandler updateCategoryCommandHandler;

    /// <summary>
    /// コンストラクタです。
    /// </summary>
    public UpdateCategoryCommandTest()
    {
        _repositoryMock = new Mock<ICategoryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        updateCategoryCommandHandler = new UpdateCategoryCommandHandler(_repositoryMock.Object);
    }

    /// <summary>
    /// カテゴリ更新コマンドのテスト
    /// </summary>
    /// <returns>true</returns>
    [Fact]
    public async Task Test_UpdateCategory_ReturnTrue()
    {
        //arrange
        var categoryData = new CategoryAggregate(
            categoryAggregateId: Guid.NewGuid(),
            colorCode: "#FFF",
            isDefault: true,
            isActive: true,
            categoryName: "test category",
            userAggregateId: Guid.NewGuid()

        );

        var command = new UpdateCategoryCommand(
            aggregateId: Guid.NewGuid(),
            colorCode: "#BBB",
            iconFilePath: "path",
            isDefault: false,
            isActive: false,
            categoryName: "category Name",
            userAggregateId: Guid.NewGuid()
        );

        _repositoryMock.Setup(m => m.FindByUserAndCategoryAggregateId(command.AggregateId, command.UserAggregateId)).ReturnsAsync(categoryData);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        //act
        var result = await updateCategoryCommandHandler.Handle(command, CancellationToken.None).ConfigureAwait(true);

        Assert.True(result);
        Assert.Equal(command.ColorCode, categoryData.ColorCode);
        Assert.Equal(command.IconFilePath, categoryData.IconFilePath);
        Assert.Equal(command.IsDefault, categoryData.IsDefault);
        Assert.Equal(command.IsActive, categoryData.IsActive);
        Assert.Equal(command.CategoryName, categoryData.CategoryItem.CategoryName);

        _repositoryMock.Verify(m => m.FindByUserAndCategoryAggregateId(command.AggregateId, command.UserAggregateId), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    /// <summary>
    /// カテゴリ更新失敗のテスト
    /// </summary>
    /// <returns>false</returns>
    [Fact]
    public async Task Test_UpdateCategory_ReturnFalse()
    {
        //arrange
        var categoryData = new CategoryAggregate(
            categoryAggregateId: Guid.NewGuid(),
            colorCode: "#FFF",
            isDefault: true,
            isActive: true,
            categoryName: "test category",
            userAggregateId: Guid.NewGuid()
        );

        var command = new UpdateCategoryCommand(
            aggregateId: Guid.NewGuid(),
            colorCode: "#BBB",
            iconFilePath: "path",
            isDefault: false,
            isActive: false,
            categoryName: "category Name",
            userAggregateId: Guid.NewGuid()
        );

        _repositoryMock.Setup(m => m.FindByUserAndCategoryAggregateId(command.AggregateId, command.UserAggregateId)).ReturnsAsync(categoryData);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        //act
        var result = await updateCategoryCommandHandler.Handle(command, CancellationToken.None).ConfigureAwait(true);

        Assert.False(result);

        _repositoryMock.Verify(m => m.FindByUserAndCategoryAggregateId(command.AggregateId, command.UserAggregateId), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    /// <summary>
    /// カテゴリNULL参照のテスト
    /// </summary>
    [Fact]
    public async Task Test_UpdateCategory_FindSubscribeThenNull()
    {
        var command = new UpdateCategoryCommand(
            aggregateId: Guid.NewGuid(),
            colorCode: "#BBB",
            iconFilePath: "path",
            isDefault: false,
            isActive: false,
            categoryName: "category Name",
            userAggregateId: Guid.NewGuid()
        );

        _repositoryMock.Setup(m => m.FindByUserAndCategoryAggregateId(command.AggregateId, command.UserAggregateId)).ReturnsAsync(() => null!);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        //act & assert
        await Assert.ThrowsAsync<NullReferenceException>(async () => await updateCategoryCommandHandler.Handle(command, CancellationToken.None).ConfigureAwait(true)).ConfigureAwait(true);
    }
}
