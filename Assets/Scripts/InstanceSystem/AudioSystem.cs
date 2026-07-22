using Fusion;

public class AudioSystem : NetworkBehaviour, IWriteSingletonsLocal
{
    public static AudioSystem Instance { get; private set; }

    public override void Spawned()
    {
        Instance = this;
        SingletonsDataWriter.Access(this).Add(this);
        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayBGM(AudioBGMType bgmType)
    {
        AudioPlayer.Instance.PlayBGM(bgmType);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_StopBGM()
    {
        AudioPlayer.Instance.StopBGM();
    }
}
