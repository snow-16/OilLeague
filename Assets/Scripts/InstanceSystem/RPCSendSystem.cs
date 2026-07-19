using Fusion;

/// <summary>
/// 外部からRPC処理を走らせるためのクラス
/// </summary>
public class RPCSendSystem : NetworkBehaviour, IWriteNetworkingLocal, IWriteSingletonsLocal
{
    /// <summary> システムのインスタンス </summary>
    public static RPCSendSystem Instance { get; private set; }

    public override void Spawned()
    {
        Instance = this;
        SingletonsDataWriter.Access(this).Add(this);
        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    /// <summary> 遷移開始を全クライアントで同期させる </summary>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayTransition()
    {
        SceneProcessor.PlayStartTransition();
    }

    /// <summary>
    /// プレイヤー退室時に各種データを並べ替える
    /// </summary>
    /// <param name="leftedPlayerNumber">退室したプレイヤーの番号</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber <= NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount)
        {
            PlayerExistServerData.DownNumber(leftedPlayerNumber);
            
            if(TryGetComponent<RoomServerData>(out var roomData))
            {
                roomData.RPC_DownPlayerNumber(leftedPlayerNumber);
            }
            if(TryGetComponent<InGameServerData>(out var inGameData))
            {
                inGameData.RPC_DownPlayerNumber(leftedPlayerNumber);
                PlayerSettingClientData.DownPlayerNumber(leftedPlayerNumber);
            }
            
            NetworkingDataWriter.Access(this).DownPlayerNumber();
        }
    }
}
