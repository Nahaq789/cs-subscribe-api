/// <summary>
/// サブスクリプション集約ルート。サブスクリプションに関する主要な情報を管理します。
/// </summary>
public class SubscribeAggregate : IAggregateRoot
{
    /// <summary>
    /// サブスクリプション集約のID
    /// </summary>
    public Guid SubscribeAggregateId { get; private set; }

    /// <summary>
    /// サブスクリプションの支払日
    /// </summary>
    public DateTime PaymentDay { get; private set; }

    /// <summary>
    /// サブスクリプションの利用開始日
    /// </summary>
    public DateTime StartDay { get; private set; }

    /// <summary>
    /// サブスクリプションの解約予定日
    /// </summary>
    public DateTime? ExpectedDateOfCancellation { get; private set; }

    /// <summary>
    /// サブスクリプションを表す色コード（16進数）
    /// UIのグラフで使用
    /// </summary>
    public string ColorCode { get; private set; }

    /// <summary>
    /// 年間契約かどうか
    /// </summary>
    public bool IsYear { get; private set; }

    /// <summary>
    /// サブスクリプションがアクティブかどうかを示すフラグ
    /// </summary>
    public bool IsActive { get; private set; }


    /// <summary>
    /// カテゴリの外部キー
    /// </summary>
    public Guid _categoryAggregateId { get; private set; }

    /// <summary>
    /// ユーザ集約ID
    /// </summary>
    public Guid _userAggregateId { get; private set; }

    /// <summary>
    /// 削除日
    /// </summary>
    public DateTime? DeleteDay { get; private set; }

    /// <summary>
    /// サブスクライブアイテム
    /// </summary>
    private SubscribeItem _subscribeItem;
    public SubscribeItem SubscribeItem => _subscribeItem;

    /// <summary>
    /// サブスクリプション集約を初期化します。
    /// </summary>
    /// <param name="subscribeAggregateId">サブスクリプション集約のID</param>
    /// <param name="paymentDay">支払日</param>
    /// <param name="startDay">利用開始日</param>
    /// <param name="colorCode">色コード（16進数）</param>
    /// <param name="isYear">年間契約かどうか</param>
    /// <param name="categoryAggregateId">カテゴリの外部キー</param>
    /// <param name="userAggregateId">ユーザ集約ID</param>
    /// <param name="expectedDateOfCancellation">解約予定日</param>
    /// <param name="deleteDay">削除日</param>
    public SubscribeAggregate(
        Guid subscribeAggregateId,
        DateTime paymentDay,
        DateTime startDay,
        string colorCode,
        bool isYear,
        Guid categoryAggregateId,
        Guid userAggregateId,
        DateTime? expectedDateOfCancellation = null,
        DateTime? deleteDay = null) : this()
    {
        SubscribeAggregateId = GeneratePrimaryKey(subscribeAggregateId);
        PaymentDay = paymentDay;
        StartDay = startDay;
        ColorCode = colorCode;
        IsYear = isYear;
        IsActive = true; // デフォルトでアクティブに設定
        _categoryAggregateId = categoryAggregateId;
        _userAggregateId = userAggregateId;
        ExpectedDateOfCancellation = expectedDateOfCancellation;
        DeleteDay = deleteDay;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected SubscribeAggregate()
    {
        this._subscribeItem = new SubscribeItem();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Guid GeneratePrimaryKey(Guid aggregateId)
    {
        if (aggregateId == Guid.Empty)
        {
            return Guid.NewGuid();
        }

        return aggregateId;
    }

    /// <summary>
    /// サブスクリプションアイテムが子エンティティと認識させるためのセッター
    /// </summary>
    /// <param name="subscribeName">サブスクリプションの名前</param>
    /// <param name="amount">サブスクリプションの金額</param>
    /// <param name="subscribeAggregateId">サブスクリプション集約ID</param>
    private void SetSubscribeItem(string subscribeName, decimal amount, Guid subscribeAggregateId)
    {
        _subscribeItem = new SubscribeItem(subscribeName, amount, subscribeAggregateId);
    }
}