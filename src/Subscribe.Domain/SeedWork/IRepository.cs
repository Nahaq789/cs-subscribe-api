/// <summary>
/// リポジトリインターフェース。
/// 集約ルートに対する永続化操作を定義します。
/// </summary>
/// <typeparam name="T">このリポジトリが扱う集約ルートの型。IAggregateRootを実装している必要があります。</typeparam>
public interface IRepository<T> where T : IAggregateRoot { }