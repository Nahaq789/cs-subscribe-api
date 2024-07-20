using Microsoft.AspNetCore.Mvc.Diagnostics;
using Moq;
using Subscribe.API.Application.Command;
using Subscribe.Domain.Model;
using Subscribe.Domain.SeedWork;
using Xunit;

namespace Subscribe.Test.Command;

/// <summary>
/// CategoryCommand のテストを提供します。
/// </summary>
public class CreateCategoryCommandTest
{
    private readonly Mock<ICategoryRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateCategoryCommandHandler createCategoryCommandHandler;

    /// <summary>
    /// コンストラクタです。
    /// </summary>
    public CreateCategoryCommandTest()
    {
        _repositoryMock = new Mock<ICategoryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        createCategoryCommandHandler = new CreateCategoryCommandHandler(_repositoryMock.Object);
    }

    /// <summary>
    /// カテゴリ作成コマンドのテスト
    /// </summary>
    /// <returns>true</returns>
    [Fact]
    public async Task Test_CreateCategory_ReturnTrue()
    {
        //arrange
        _repositoryMock.Setup(m => m.AddAsync(It.IsAny<CategoryAggregate>())).Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        var command = new CreateCategoryCommand(
            colorCode: "#FFFFFF",
            iconFilePath: "path",
            isDefault: true,
            isActive: true,
            categoryName: "test category",
            userAggregateId: Guid.NewGuid()
        );

        //act
        var result = await createCategoryCommandHandler.Handle(command, CancellationToken.None).ConfigureAwait(true);

        //assert
        Assert.True(result);
        _repositoryMock.Verify(m => m.AddAsync(It.IsAny<CategoryAggregate>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// カテゴリ作成コマンドのテスト
    /// </summary>
    /// <returns>false</returns>
    [Fact]
    public async Task Test_CreateCategory_ReturnFalse()
    {
        //arrange
        _repositoryMock.Setup(m => m.AddAsync(It.IsAny<CategoryAggregate>())).Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        var command = new CreateCategoryCommand(
            colorCode: "#FFFFFF",
            iconFilePath: "path",
            isDefault: true,
            isActive: true,
            categoryName: "test category",
            userAggregateId: Guid.NewGuid()
        );

        //act
        var result = await createCategoryCommandHandler.Handle(command, CancellationToken.None).ConfigureAwait(true);

        //assert
        Assert.False(result);
        _repositoryMock.Verify(m => m.AddAsync(It.IsAny<CategoryAggregate>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
