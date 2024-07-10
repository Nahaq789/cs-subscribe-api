namespace Subscribe.Domain.Model;

/// <summary>
/// カテゴリー項目を表すクラス
/// カテゴリーの基本情報と集約IDを保持します
/// </summary>
public class CategoryItem
{
    /// <summary>
    /// カテゴリーの主キー
    /// </summary>
    public long CategoryItemId { get; private set; }

    /// <summary>
    /// カテゴリーの名前
    /// </summary>
    public string CategoryName { get; private set; }

    /// <summary>
    /// カテゴリー集約の外部キー
    /// </summary>
    public Guid CategoryAggregateId { get; private set; }

    /// <summary>
    /// CategoryItemクラスの新しいインスタンスを初期化します
    /// </summary>
    /// <param name="categoryName">カテゴリーの名前</param>
    /// <param name="categoryAggregateId">カテゴリー集約の外部キー</param>
    public CategoryItem(string categoryName, Guid categoryAggregateId)
    {
        CategoryName = categoryName;
        CategoryAggregateId = categoryAggregateId;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public CategoryItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}