using System.Diagnostics;
using Debug = UnityEngine.Debug;

/// <summary>
/// InputListLocalDataの書き換えクラス
/// </summary>
public class NetworkingDataWriter
{
    /// <summary> データクラスのインスタンス </summary>
    public NetworkingLocalData Data { get; }

    private NetworkingDataWriter()
    {
        Data = NetworkingLocalData.Access();
    }

    /// <summary>
    /// インスタンスの取得。
    /// ゲームマネージャー・入力受け取りクラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static NetworkingDataWriter Access()
    {
        var accessedClass = new StackFrame(1).GetMethod()?.ReflectedType;
        if(typeof(IWriteNetworkingLocal).IsAssignableFrom(accessedClass) || accessedClass == typeof(GameManager))
        {
            return new NetworkingDataWriter();
        }

        Debug.LogError("アクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// データの初期化
    /// </summary>
    public void Reset()
    {
        Data.SetRunner(null);
    }

    /// <summary>
    /// プレイヤー番号の割り当て
    /// </summary>
    public void AssignPlayerNumber()
    {
        Data.SetPlayerNumber(NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount);
    }
}
