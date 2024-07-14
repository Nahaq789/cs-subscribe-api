using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Subscribe.Domain.Model.SubscribeAggregate;
using Subscribe.Domain.SeedWork;
using Subscribe.Infrastructure.Context;
using Subscribe.Infrastructure.Repositories;
using Xunit;

namespace Subscribe.Infrastructure.Test.Repositories;

/// <summary>
/// SubscribeRepository のテストを提供します。
/// </summary>
public class SubscribeRepositoryTest
{
    private readonly Mock<SubscribeContext> _contextMock;
    private readonly Mock<DbSet<SubscribeAggregate>> _dbSetMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    /// <summary>
    /// コンストラクタです。
    /// </summary>
    public SubscribeRepositoryTest()
    {
        _contextMock = new Mock<SubscribeContext>(new DbContextOptions<SubscribeContext>());
        _dbSetMock = new Mock<DbSet<SubscribeAggregate>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    /// <summary>
    /// コンストラクタパラメータがNULLの場合、例外がスローされることをテストします。
    /// </summary>
    [Fact]
    public void Test_Constructor_NullContext_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
#pragma warning disable CS8625
        Assert.Throws<ArgumentNullException>(() => new SubscribeRepository(null));
#pragma warning restore CS8625
    }

    /// <summary>
    /// SubscribeRepository のインスタンスが正しく作成されることをテストします。
    /// </summary>
    [Fact]
    public void Test_CreateConstructor_Success()
    {
        //act
        var repository = new SubscribeRepository(_contextMock.Object);

        //assert
        Assert.NotNull(repository);
    }

    /// <summary>
    /// UnitOfWorkプロパティが正しくSubscribeContextを返すことをテストします。
    /// </summary>
    [Fact]
    public void Test_UnitOfWork_ReturnsContext()
    {
        //arrange
        var repository = new SubscribeRepository(_contextMock.Object);

        //act
        var result = repository.UnitOfWork;

        //assert
        Assert.Same(_contextMock.Object, result);
    }
}
