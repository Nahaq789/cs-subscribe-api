/// <summary>
/// 集約ルートのみ実装する
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    /// 集約ルートの主キーを生成します。
    /// </summary>
    Guid GeneratePrimaryKey(Guid aggregateId);
}