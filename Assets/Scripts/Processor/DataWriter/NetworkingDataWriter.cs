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
        Data = NetworkingLocalData.Access(this);
    }

    /// <summary>
    /// インスタンスの取得。
    /// ゲームマネージャー・入力受け取りクラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static NetworkingDataWriter Access(object accessed)
    {
        if(accessed is IWriteNetworkingLocal || accessed is GameManager)
        {
            return new NetworkingDataWriter();
        }

        Debug.LogError($"{nameof(NetworkingDataWriter)}へのアクセス権限がありません。");
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
