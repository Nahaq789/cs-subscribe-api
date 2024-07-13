using Microsoft.EntityFrameworkCore;

namespace Subscribe.Test.Infrastructure.Context;
public class SubscribeDbContextTest
{
    private SubscribeContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<SubscribeContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new SubscribeContext(options);
    }

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