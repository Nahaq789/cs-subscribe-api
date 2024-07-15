using Moq;
using Subscribe.API.Application.Command;
using Subscribe.Domain.Model.SubscribeAggregate;
using Subscribe.Domain.SeedWork;
using Xunit;

namespace Subscribe.Test.Command;

/// <summary>
/// SubscribeCommand のテストを提供します。
/// </summary>
public class DeleteSubscribeCommandTest
{
    private readonly Mock<ISubscribeRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteSubscribeCommandHandler _handler;
  
    /// <summary>
    /// コンストラクタです。
    /// </summary>
    public DeleteSubscribeCommandTest()
    {
        _repositoryMock = new Mock<ISubscribeRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteSubscribeCommandHandler(_repositoryMock.Object);
    }
    
    /// <summary>
    /// サブスクライブ削除コマンドのテスト
    /// </summary>
    [Fact]
    public async Task Test_DeleteSubscribe_ReturnTrue()
    {
        //arrange
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
        var _command = new DeleteSubscribeCommand(
            subscribeAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid()
        );
        _repositoryMock.Setup(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId))
            .ReturnsAsync(subscribeData);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        
        //act
        var result = await _handler.Handle(_command, CancellationToken.None);
        
        //assert
        Assert.True(result);
        _repositoryMock.Verify(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    /// <summary>
    /// サブスクライブ削除コマンドのテスト
    /// </summary>
    /// <returns>true</returns>
    [Fact]
    public async Task Test_DeleteSubscribe_ReturnFalse()
    {
        //arrange
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
        var _command = new DeleteSubscribeCommand(
            subscribeAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid()
        );
        _repositoryMock.Setup(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId))
            .ReturnsAsync(subscribeData);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _unitOfWorkMock.Setup(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        
        //act
        var result = await _handler.Handle(_command, CancellationToken.None);
        
        //assert
        Assert.False(result);
        _repositoryMock.Verify(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId), Times.Once());
        _unitOfWorkMock.Verify(m => m.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    /// <summary>
    /// サブスクライブ更新コマンドのテスト
    /// </summary>
    [Fact]
    public async Task Test_DeleteSubscribe_FindSubscribeThenNull()
    {
        //arrange
        var _command = new DeleteSubscribeCommand(
            subscribeAggregateId: Guid.NewGuid(),
            userAggregateId: Guid.NewGuid()
        );
        
        _repositoryMock.Setup(m => m.FindBySubscribeAgIdAndUserAgId(_command.SubscribeAggregateId, _command.UserAggregateId)).ReturnsAsync(() => null!);
        
        //act & assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _handler.Handle(_command, CancellationToken.None));
    }
}