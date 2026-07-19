using System.Linq;
using Fusion;
using UniRx;

/// <summary>
/// ウェイティングルームシーン内でネットワーク同期するデータ
/// </summary>
public class RoomServerData : NetworkBehaviour, IWriteSingletonsLocal
{
    /// <summary> データのインスタンス </summary>
    public static RoomServerData Instance { get; private set; } = null;

    /// <summary> データの保存が完了したクライアントの数 </summary>
    [Networked]
    public int SaveFinished { get; private set; }
    /// <summary> 各プレイヤーの設定情報 </summary>
    [Networked, Capacity(5)]
    public NetworkArray<PlayerSettings> Players => default;

    public override void Spawned()
    {
        Instance = this;
        SingletonsDataWriter.Access(this).Add(this);
        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    /// <summary>
    /// プレイヤーの陣営を変更する
    /// </summary>
    /// <param name="playerNumber">変更するプレイヤーの番号</param>
    /// <param name="type">変更先の陣営</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_UpdateType(int playerNumber, SpinnerType type)
    {
        var copy = Players[playerNumber - 1];
        copy.type = type;
        Players.Set(playerNumber - 1, copy);
    }

    /// <summary>
    /// データをクライアント側で保存する
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SaveData()
    {
        PlayerSettingClientData.ReceiveFromServer(Players.ToList());
        RPC_SaveFinished();
    }

    /// <summary>
    /// 保存完了を通知する
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SaveFinished()
    {
        SaveFinished++;
    }

    /// <summary>
    /// プレイヤー退室時にデータを並べ替える
    /// </summary>
    /// <param name="leftedPlayerNumber">退室したプレイヤーの番号</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber < NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount)
        {
            RPC_UpdateType(leftedPlayerNumber, Players[leftedPlayerNumber].type);
            RPC_DownPlayerNumber(leftedPlayerNumber + 1);
        }
        else
        {
            RPC_UpdateType(leftedPlayerNumber, SpinnerType.None);
        }
    }

    /// <summary>
    /// プレイヤーの設定情報
    /// </summary>
    public struct PlayerSettings : INetworkStruct
    {
        /// <summary> 陣営 </summary>
        public SpinnerType type;
    }
}
