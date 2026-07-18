using Fusion;

public class RPCSendSystem : NetworkBehaviour, IWriteNetworkingLocal, IWriteSingletonsLocal
{
    public static RPCSendSystem Instance { get; private set; }

    public override void Spawned()
    {
        Instance = this;
        SingletonsDataWriter.Access(this).Add(this);
        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayTransition()
    {
        SceneProcessor.PlayTransition(() => {});
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber <= NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount)
        {
            SingletonsLocalData.Singletons.ForEach(singleton => singleton.Object.RequestStateAuthority());
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
