using UniRx;


/// <summary>
/// インゲームシーンが読み込まれたことを検知するクラス
/// </summary>
public class InGameLoadedAnker : SceneLoadedAnker
{
    protected override async void WhenLoaded()
    {
        await NetworkingProcessor.SpawnObject(SpinnerTypeDataBase.Data.SpinnerPrefab, (runner, obj) => obj.GetComponent<SpinnerInstanceData>().RPC_SetType(SpinnerLocalData.Type));

        Observable.EveryUpdate().Where(_ => InGameServerData.Instance != null).First().Subscribe(_ => InGameServerData.Instance.RPC_StartTimer(GeneralDataBase.Data.DefaultTimeLimit));
    }
}
