using Fusion;

/// <summary>
/// ネットワーク間での音響同期用クラス
/// </summary>
public class AudioSystem : NetworkBehaviour, IWriteSingletonsLocal
{
    /// <summary> インスタンス </summary>
    public static AudioSystem Instance { get; private set; }

    public override void Spawned()
    {
        Instance = this;
        SingletonsDataWriter.Access(this).Add(this);
        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    /// <summary>
    /// 任意のBGMの再生
    /// </summary>
    /// <param name="bgmType">BGMの種類</param>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayBGM(AudioBGMType bgmType)
    {
        AudioPlayer.Instance.PlayBGM(bgmType);
    }

    /// <summary>
    /// BGMの停止
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_StopBGM()
    {
        AudioPlayer.Instance.StopBGM();
    }
}
