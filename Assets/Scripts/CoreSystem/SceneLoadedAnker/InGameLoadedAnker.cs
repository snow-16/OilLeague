using UniRx;


/// <summary>
/// インゲームシーンが読み込まれたことを検知するクラス
/// </summary>
public class InGameLoadedAnker : SceneLoadedAnker
{
    protected override async void WhenLoaded()
    {
        await NetworkingProcessor.SpawnObject(SpinnerTypeDataBase.Data.SpinnerPrefab, (runner, obj) => obj.GetComponent<SpinnerInstanceData>().RPC_SetType(SpinnerLocalData.Type));

        Observable.EveryUpdate().Where(_ => Generated == _networkedPrefabs.Count + NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount).First().Subscribe(_ =>
            {
                SceneProcessor.ChangeState(SceneState.TransitionEnd);
                
                this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Exist).Subscribe(_ =>
                    {
                        InGameServerData.Instance.RPC_StartTimer(GeneralDataBase.Data.DefaultTimeLimit);
                    }
                );
            }
        );
    }
}
