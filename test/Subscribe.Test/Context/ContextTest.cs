using Microsoft.EntityFrameworkCore;
using Subscribe.Infrastructure.Context;
using Xunit;

namespace Subscribe.Infrastructure.Test.Context;

/// <summary>
/// SubscribeDbContext �̃e�X�g��񋟂��܂��B
/// </summary>
public class SubscribeDbContextTest
{
    /// <summary>
    /// �C���������f�[�^�x�[�X�I�v�V�������g�p���ĐV���� SubscribeContext �̃C���X�^���X���쐬���܂��B
    /// </summary>
    /// <returns>�V���� SubscribeContext �̃C���X�^���X�B</returns>
    private static SubscribeContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<SubscribeContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new SubscribeContext(options);
    }

    /// <summary>
    /// �f�[�^�x�[�X�ւ̐ڑ����e�X�g���܂��B
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
    /// �C���������f�[�^�x�[�X�Ɋ��҂����e�[�u�������݂��邱�Ƃ��e�X�g���܂��B
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