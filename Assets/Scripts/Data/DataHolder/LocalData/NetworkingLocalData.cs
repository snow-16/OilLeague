using System.Collections.Generic;
using System.Diagnostics;
using Fusion;
using Debug = UnityEngine.Debug;

/// <summary>
/// ネットワーク処理用のローカルデータ
/// </summary>
public class NetworkingLocalData
{
    /// <summary> ネットワークランナーインスタンス </summary>
    public static NetworkRunner NetworkRunner { get; private set; }
    /// <summary> 現在立っている部屋リストのキャッシュ </summary>
    public static List<SessionInfo> AllSessions { get; private set; }
    /// <summary> プレイヤー番号 </summary>
    public static int PlayerNumber { get; private set; }

    private NetworkingLocalData()
    {
        
    }

    /// <summary>
    /// インスタンスの取得。
    /// ライタークラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static NetworkingLocalData Access(object accessed)
    {
        if(accessed is NetworkingDataWriter)
        {
            return new NetworkingLocalData();
        }

        Debug.LogError($"{nameof(NetworkingLocalData)}へのアクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// ネットワークランナー書き換え
    /// </summary>
    /// <param name="value">ネットワークランナーインスタンス</param>
    public void SetRunner(NetworkRunner value)
    {
        NetworkRunner = value;
    }

    /// <summary>
    /// セッションリスト書き換え
    /// </summary>
    /// <param name="value">ネットワークランナーインスタンス</param>
    public void SetAllSessions(List<SessionInfo> value)
    {
        AllSessions = value;
    }

    /// <summary>
    /// プレイヤー番号書き換え
    /// </summary>
    /// <param name="value">番号</param>
    public void SetPlayerNumber(int value)
    {
        PlayerNumber = value;
    }
}
