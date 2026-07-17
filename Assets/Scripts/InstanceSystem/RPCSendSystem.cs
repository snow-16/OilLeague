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

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_DownPlayerNumber()
    {
        if(NetworkingLocalData.PlayerNumber == 2)
        {
            SingletonsLocalData.Singletons.ForEach(singleton => singleton.Object.RequestStateAuthority());
            
            if(TryGetComponent<RoomServerData>(out var roomData))
            {
                roomData.RPC_DownPlayerNumber(NetworkingLocalData.PlayerNumber);
            }
            if(TryGetComponent<InGameServerData>(out var inGameData))
            {
                inGameData.RPC_DownPlayerNumber(NetworkingLocalData.PlayerNumber);
            }
            
            NetworkingDataWriter.Access(this).DownPlayerNumber();
            PlayerSettingClientData.DownPlayerNumber(NetworkingLocalData.PlayerNumber);
        }
    }
}
