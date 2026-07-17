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
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        NetworkingDataWriter.Access(this).DownPlayerNumber();
        PlayerSettingClientData.DownPlayerNumber(leftedPlayerNumber);
    }
}
