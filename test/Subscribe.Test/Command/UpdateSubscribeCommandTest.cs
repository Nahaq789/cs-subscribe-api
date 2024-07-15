using Moq;
using Subscribe.API.Application.Command.Subscribe;
using Subscribe.Domain.Model.SubscribeAggregate;
using Subscribe.Domain.SeedWork;
using Xunit;

namespace Subscribe.Test.Command;

/// <summary>
/// SubscribeCommand のテストを提供します。
/// </summary>
public class UpdateSubscribeCommandTest
{
    private readonly Mock<ISubscribeRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<SubscribeAggregate> _aggregateMock;
    private readonly UpdateSubscribeCommandHandler _handler;

    /// <summary>
    /// コンストラクタです。
    /// </summary>
    public UpdateSubscribeCommandTest()
    {
        _repositoryMock = new Mock<ISubscribeRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _aggregateMock = new Mock<SubscribeAggregate>();
        _handler = new UpdateSubscribeCommandHandler(_repositoryMock.Object);
    }

    /// <summary>
    /// サブスクライブ更新コマンドのテスト
    /// </summary>
    /// <returns>true</returns>
    [Fact]
    public async Task Test_UpdateSubscribe_ReturnTrue()
    {
        // arrange
        var subscribeData = new SubscribeAggregate(
            subscribeAggregateId: Guid.NewGuid(),
            paymentDay: DateTime.UtcNow,
            startDay: DateTime.UtcNow.AddDays(1),
            colorCode: "#FFFFFF",
            isYear: true,
            categoryAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid(),
            isActive: true,
            expectedDateOfCancellation: DateTime.UtcNow.AddYears(1),
            deleteDay: DateTime.UtcNow.AddYears(2)
        );

        var _command = new UpdateSubscribeCommand(
            subscribeAggregateId: Guid.NewGuid(),
            paymentDay: DateTime.UtcNow,
            startDay: DateTime.UtcNow.AddDays(1),
            colorCode: "#FFFFFF",
            isYear: true,
            isActive: true,
            categoryAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid(),
            subscribeName: "Test Subscription",
            amount: 99.99m,
            expectedDateOfCancellation: DateTime.UtcNow.AddYears(1)
        );
        _repositoryMock.Setup(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId)).ReturnsAsync(subscribeData);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

        //act
        var result = await _handler.Handle(_command, CancellationToken.None).ConfigureAwait(true);

        //assert
        Assert.True(result);
        Assert.Equal(_command.PaymentDay, subscribeData.PaymentDay);
        Assert.Equal(_command.StartDay, subscribeData.StartDay);
        Assert.Equal(_command.ColorCode, subscribeData.ColorCode);
        Assert.Equal(_command.IsYear, subscribeData.IsYear);
        Assert.Equal(_command.IsActive, subscribeData.IsActive);
        Assert.Equal(_command.CategoryAggregateId, subscribeData._categoryAggregateId);
        Assert.Equal(_command.UserAggregateId, subscribeData._userAggregateId);
        Assert.Equal(_command.ExpectedDateOfCancellation, subscribeData.ExpectedDateOfCancellation);
        _repositoryMock.Verify(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// サブスクライブ更新コマンドのテスト
    /// </summary>
    /// <returns>false</returns>
    [Fact]
    public async Task Test_UpdateSubscribe_ReturnFalse()
    {
        // arrange
        var subscribeData = new SubscribeAggregate(
            subscribeAggregateId: Guid.NewGuid(),
            paymentDay: DateTime.UtcNow,
            startDay: DateTime.UtcNow.AddDays(1),
            colorCode: "#FFFFFF",
            isYear: true,
            categoryAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid(),
            isActive: true,
            expectedDateOfCancellation: DateTime.UtcNow.AddYears(1),
            deleteDay: DateTime.UtcNow.AddYears(2)
        );

        var _command = new UpdateSubscribeCommand(
            subscribeAggregateId: Guid.NewGuid(),
            paymentDay: DateTime.UtcNow,
            startDay: DateTime.UtcNow.AddDays(1),
            colorCode: "#FFFFFF",
            isYear: true,
            isActive: true,
            categoryAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid(),
            subscribeName: "Test Subscription",
            amount: 99.99m,
            expectedDateOfCancellation: DateTime.UtcNow.AddYears(1)
        );
        _repositoryMock.Setup(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId)).ReturnsAsync(subscribeData);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);

        //act
        var result = await _handler.Handle(_command, CancellationToken.None).ConfigureAwait(true);

        //assert
        Assert.False(result);
        Assert.Equal(_command.PaymentDay, subscribeData.PaymentDay);
        Assert.Equal(_command.StartDay, subscribeData.StartDay);
        Assert.Equal(_command.ColorCode, subscribeData.ColorCode);
        Assert.Equal(_command.IsYear, subscribeData.IsYear);
        Assert.Equal(_command.IsActive, subscribeData.IsActive);
        Assert.Equal(_command.CategoryAggregateId, subscribeData._categoryAggregateId);
        Assert.Equal(_command.UserAggregateId, subscribeData._userAggregateId);
        Assert.Equal(_command.ExpectedDateOfCancellation, subscribeData.ExpectedDateOfCancellation);
        _repositoryMock.Verify(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// サブスクライブ更新コマンドのテスト
    /// </summary>
    [Fact]
    public async Task Test_UpdateSubscribe_FindSubscribeThenNull()
    {
        // arrange
        var _command = new UpdateSubscribeCommand(
            subscribeAggregateId: Guid.NewGuid(),
            paymentDay: DateTime.UtcNow,
            startDay: DateTime.UtcNow.AddDays(1),
            colorCode: "#FFFFFF",
            isYear: true,
            isActive: true,
            categoryAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid(),
            subscribeName: "Test Subscription",
            amount: 99.99m,
            expectedDateOfCancellation: DateTime.UtcNow.AddYears(1)
        );
        _repositoryMock.Setup(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId)).ReturnsAsync(() => null!);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);

        //act & assert
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _handler.Handle(_command, CancellationToken.None).ConfigureAwait(true)).ConfigureAwait(true);
    }
}
