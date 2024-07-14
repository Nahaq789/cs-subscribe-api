
using Moq;
using Subscribe.API.Application.Command;
using Subscribe.Domain.SeedWork;
using Xunit;

namespace Subscribe.Test.Command;

/// <summary>
/// SubscribeCommand のテストを提供します。
/// </summary>
public class CreateSubscribeCommandTest
{
    private readonly Mock<ISubscribeRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateSubscribeCommandHandler createSubscribeCommandHandler;

    /// <summary>
    /// コンストラクタです。
    /// </summary>
    public CreateSubscribeCommandTest()
    {
        _repositoryMock = new Mock<ISubscribeRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        createSubscribeCommandHandler = new(_repositoryMock.Object);
    }

    /// <summary>
    /// サブスクライブ作成コマンドのテスト
    /// </summary>
    /// <returns>true</returns>
    [Fact]
    public async Task Test_CreateSubscribe_ReturnTrue()
    {
        //arrange
        _repositoryMock.Setup(m => m.AddAsync(It.IsAny<SubscribeAggregate>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        var command = new CreateSubscribeCommand(
            paymentDay: DateTime.UtcNow,
            startDay: DateTime.UtcNow.AddDays(1),
            colorCode: "#FFFFFF",
            isYear: true,
            categoryAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid(),
            subscribeName: "Test Subscription",
            amount: 99.99m,
            expectedDateOfCancellation: DateTime.UtcNow.AddYears(1)
        );

        //act
        var result = await createSubscribeCommandHandler.Handle(command, CancellationToken.None).ConfigureAwait(true);

        //assert
        Assert.True(result);
        _repositoryMock.Verify(m => m.AddAsync(It.IsAny<SubscribeAggregate>()), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    /// <summary>
    /// サブスクライブ作成コマンドのテスト
    /// </summary>
    /// <returns>false</returns>
    [Fact]
    public async Task Test_CreateSubscribe_ReturnFalse()
    {
        //arrange
        _repositoryMock.Setup(m => m.AddAsync(It.IsAny<SubscribeAggregate>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        var command = new CreateSubscribeCommand(
            paymentDay: DateTime.UtcNow,
            startDay: DateTime.UtcNow.AddDays(1),
            colorCode: "#FFFFFF",
            isYear: true,
            categoryAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid(),
            subscribeName: "Test Subscription",
            amount: 99.99m,
            expectedDateOfCancellation: DateTime.UtcNow.AddYears(1)
        );

        //act
        var result = await createSubscribeCommandHandler.Handle(command, CancellationToken.None).ConfigureAwait(true);

        //assert
        Assert.False(result);
        _repositoryMock.Verify(m => m.AddAsync(It.IsAny<SubscribeAggregate>()), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
}
