/// <summary>
/// ルームシーンが読み込まれたことを検知するクラス
/// </summary>
public class WaitingRoomLoadedAnker : SceneLoadedAnker
{
    protected override void WhenLoaded()
    {
        SetGenerateEndTrigger(() => GeneratedCount == _networkedPrefabs.Count, () => AudioSystem.Instance.RPC_PlayBGM(AudioBGMType.Waiting));
    }
}
