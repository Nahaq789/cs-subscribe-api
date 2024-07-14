using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Subscribe.Infrastructure.Test.Context;

/// <summary>
/// SubscribeDbContext のテストを提供します。
/// </summary>
public class SubscribeDbContextTest
{
    /// <summary>
    /// インメモリデータベースオプションを使用して新しい SubscribeContext のインスタンスを作成します。
    /// </summary>
    /// <returns>新しい SubscribeContext のインスタンス。</returns>
    private static SubscribeContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<SubscribeContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new SubscribeContext(options);
    }

    /// <summary>
    /// データベースへの接続をテストします。
    /// </summary>
    [Fact]
    public void Test_ConnectToDatabase_Success()
    {
        //act & assert
        using (var context = CreateContext())
        {
            bool isConnect = context.Database.CanConnect();
            Assert.True(isConnect);
        }
    }

    /// <summary>
    /// インメモリデータベースに期待されるテーブルが存在することをテストします。
    /// </summary>
    [Fact]
    public void Test_DatabaseHasExpectedTables()
    {
        //act & assert
        using (var context = CreateContext())
        {
            var tableNames = context.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .ToList();

            Assert.Contains("category_aggregate", tableNames);
            Assert.Contains("category_item", tableNames);
            Assert.Contains("subscribe_aggregate", tableNames);
            Assert.Contains("subscribe_item", tableNames);
        }
    }
}