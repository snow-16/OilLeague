using System.Collections.Generic;

/// <summary>
/// 各プレイヤーの設定情報データ
/// </summary>
public class PlayerSettingClientData
{
    /// <summary> 各プレイヤーの設定情報 </summary>
    public static List<RoomServerData.PlayerSettings> Players { get; private set; }
    /// <summary> 時間制限 </summary>
    public static int TimeLimit { get; private set; }

    /// <summary>
    /// ネットワーク同期されたデータをローカルデータとして受け取る
    /// </summary>
    /// <param name="players">各プレイヤーの設定情報</param>
    public static void ReceiveFromServer(List<RoomServerData.PlayerSettings> players, int timeLimit)
    {
        Players = new(players);
        TimeLimit = timeLimit;
    }

    /// <summary>
    /// プレイヤー退室時にデータを並べ替える
    /// </summary>
    /// <param name="leftedPlayerNumber">退室したプレイヤーの番号</param>
    public static void DownPlayerNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber < NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount)
        {
            Players[leftedPlayerNumber - 1] = Players[leftedPlayerNumber];
            DownPlayerNumber(leftedPlayerNumber + 1);
        }
        else
        {
            Players[leftedPlayerNumber - 1] = new();
        }
    }
}
