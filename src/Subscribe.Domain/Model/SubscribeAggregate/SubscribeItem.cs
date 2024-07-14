using Subscribe.Domain.SeedWork;

namespace Subscribe.Domain.Model.SubscribeAggregate;

/// <summary>
/// サブスクリプションアイテムを表すクラス。
/// 各サブスクリプションの詳細情報を保持します。
/// </summary>
public class SubscribeItem : Entity
{
    /// <summary>
    /// サブスクリプションアイテムの主キー
    /// </summary>
    public long SubscribeItemId { get; private set; }

    /// <summary>
    /// サブスクリプションの名前
    /// </summary>
    public string SubscribeName { get; private set; }

    /// <summary>
    /// サブスクリプションの金額
    /// </summary>
    public decimal Amount;

    /// <summary>
    /// サブスクリプション集約ID
    /// </summary>
    public Guid SubscribeAggregateId { get; private set; }

    // public SubscribeAggregate SubscribeAggregate { get; private set; }

    /// <summary>
    /// サブスクリプションアイテムを初期化します。
    /// </summary>
    /// <param name="subscribeName">サブスクリプションの名前</param>
    /// <param name="amount">サブスクリプションの金額</param>
    /// <param name="subscribeAggregateId">サブスクリプション集約ID</param>
    public SubscribeItem(string subscribeName, decimal amount, Guid subscribeAggregateId)
    {
        SubscribeName = subscribeName;
        SetAmount(amount);
        SubscribeAggregateId = subscribeAggregateId;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public SubscribeItem() { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    /// <summary>
    /// サブスクリプションの金額を設定します。
    /// </summary>
    /// <param name="amount">設定する金額</param>
    /// <exception cref="SubscribeDomainException">金額が負の値の場合にスローされます。</exception>
    public void SetAmount(decimal amount)
    {
        if (amount < 0)
        {
            throw new SubscribeDomainException("金額は0円以上でなければなりません。");
        }
        Amount = amount;
    }
}