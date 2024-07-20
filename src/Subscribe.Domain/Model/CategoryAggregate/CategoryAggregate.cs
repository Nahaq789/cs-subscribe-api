using Subscribe.Domain.Exceptions;

namespace Subscribe.Domain.Model;

/// <summary>
/// カテゴリーに関する情報を保持するクラス
/// </summary>
public class CategoryAggregate : IAggregateRoot
{
    /// <summary>
    /// カテゴリー集約のID
    /// </summary>
    public Guid CategoryAggregateId { get; private set; }

    /// <summary>
    /// カテゴリーを表す色コード（16進数）
    /// UI側で使用
    /// </summary>
    public string ColorCode { get; private set; }

    /// <summary>
    /// アイコンファイルへのパス
    /// </summary>
    public string? IconFilePath { get; private set; }

    /// <summary>
    /// このカテゴリーがデフォルトかどうかを示すフラグ
    /// </summary>
    public bool IsDefault { get; private set; }

    /// <summary>
    /// このカテゴリーがアクティブかどうかを示すフラグ
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// ユーザ集約ID
    /// </summary>
    public Guid UserAggregateId { get; private set; }

    /// <summary>
    /// カテゴリーアイテム
    /// </summary>
    private CategoryItem _categoryItem;

    public CategoryItem CategoryItem => _categoryItem;

    /// <summary>
    /// Categoryクラスの新しいインスタンスを初期化します
    /// </summary>
    /// <param name="categoryAggregateId">カテゴリー集約のID</param>
    /// <param name="colorCode">色コード（16進数）</param>
    /// <param name="iconFilePath">アイコンファイルへのパス</param>
    /// <param name="isDefault">このカテゴリーがデフォルトかどうか</param>
    /// <param name="isActive">このカテゴリーがアクティブかどうか</param>
    /// <param name="categoryName">カテゴリの名前</param>
    /// <param name="userAggregateId">ユーザ集約ID</param>
    public CategoryAggregate(Guid categoryAggregateId, string colorCode, bool isDefault, bool isActive, string categoryName, Guid userAggregateId, string? iconFilePath = null) : this()
    {
        CategoryAggregateId = GeneratePrimaryKey(categoryAggregateId);
        ColorCode = colorCode;
        IsDefault = isDefault;
        IsActive = isActive;
        IconFilePath = iconFilePath;
        UserAggregateId = userAggregateId;

        SetCategoryItem(categoryName, this.CategoryAggregateId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    protected CategoryAggregate()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _categoryItem = new CategoryItem();
    }

    public Guid GeneratePrimaryKey(Guid aggregateId)
    {
        if (aggregateId == Guid.Empty)
        {
            return Guid.NewGuid();
        }

        return aggregateId;
    }

    /// <summary>
    /// カテゴリーアイテムが子エンティティと認識させるためのセッター
    /// </summary>
    /// <param name="categoryName">カテゴリーの名前</param>
    /// <param name="categoryAggregateId">カテゴリー集約の外部キー</param>
    private void SetCategoryItem(string categoryName, Guid categoryAggregateId)
    {
        _categoryItem = new CategoryItem(categoryName, categoryAggregateId);
    }

    public void UpdateCategoryAggregate(string colorCode, bool isDefault, bool isActive, string iconFilePath)
    {
        ColorCode = colorCode;
        IsDefault = isDefault;
        IsActive = isActive;
        IconFilePath = iconFilePath;
    }

    public void UpdateCategoryItem(string categoryName, Guid categoryAggregateId)
    {
        if (_categoryItem == null)
        {
            throw new CategoryDomainException($"集約ID: {CategoryItem.CategoryAggregateId} が見つかりませんでした。");
        }

        CategoryItem.UpdateCategoryItem(categoryName, categoryAggregateId);
    }
}