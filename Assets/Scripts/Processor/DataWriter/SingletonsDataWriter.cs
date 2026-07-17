using Fusion;
using Debug = UnityEngine.Debug;

/// <summary>
/// InputListLocalDataの書き換えクラス
/// </summary>
public class SingletonsDataWriter
{
    /// <summary> データクラスのインスタンス </summary>
    public SingletonsLocalData Data { get; }

    private SingletonsDataWriter()
    {
        Data = SingletonsLocalData.Access(this);
    }

    /// <summary>
    /// インスタンスの取得。
    /// ゲームマネージャー・入力受け取りクラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static SingletonsDataWriter Access(object accessed)
    {
        if(accessed is IWriteSingletonsLocal || accessed is GameManager)
        {
            return new SingletonsDataWriter();
        }

        Debug.LogError($"{nameof(SingletonsDataWriter)}へのアクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// データの初期化
    /// </summary>
    public void Reset()
    {
        Data.SetSingletons(new());
    }

    /// <summary>
    /// インスタンス追加
    /// </summary>
    /// <param name="instance">インスタンス</param>
    public void Add(NetworkBehaviour instance)
    {
        var copy = SingletonsLocalData.Singletons;
        copy.Add(instance);
        Data.SetSingletons(copy);
    }
}
