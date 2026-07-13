using Fusion;

public class RPCSendSystem : NetworkBehaviour
{
    public static RPCSendSystem Instance { get; private set; }

    public override void Spawned()
    {
        Instance = this;
        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayTransition()
    {
        SceneProcessor.PlayTransition(() => {});
    }
}
