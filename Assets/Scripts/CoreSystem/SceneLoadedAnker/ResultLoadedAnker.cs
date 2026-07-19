/// <summary>
/// リザルトシーンが読み込まれたことを検知するクラス
/// </summary>
public class ResultLoadedAnker : SceneLoadedAnker
{
    protected override void WhenLoaded()
    {
        SetGenerateEndTrigger(() => GeneratedCount == _networkedPrefabs.Count);
    }
}
