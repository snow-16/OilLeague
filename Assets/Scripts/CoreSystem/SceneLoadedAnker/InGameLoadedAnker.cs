/// <summary>
/// インゲームシーンが読み込まれたことを検知するクラス
/// </summary>
public class InGameLoadedAnker : SceneLoadedAnker
{
    protected override async void WhenLoaded()
    {
        await NetworkingProcessor.SpawnObject(SpinnerTypeDataBase.Data.SpinnerPrefab, (runner, obj) => obj.GetComponent<SpinnerInstanceData>().SetType(SpinnerLocalData.Type));
    }
}
