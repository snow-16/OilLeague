using UniRx;
using UnityEngine;


/// <summary>
/// インゲームシーンが読み込まれたことを検知するクラス
/// </summary>
public class InGameLoadedAnker : SceneLoadedAnker
{
    protected override async void WhenLoaded()
    {
        await NetworkingProcessor.SpawnObjectAtPosition(SpinnerTypeDataBase.Data.SpinnerPrefab, Quaternion.Euler(0, 0, 360 / NetworkingLocalData.PlayerNumber / NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount) * Vector2.up * GeneralDataBase.Data.FieldRadius * 0.8f, (runner, obj) => obj.GetComponent<SpinnerInstanceData>().RPC_SetType(SpinnerLocalData.Type));

        Observable.EveryUpdate().Where(_ => Generated == _networkedPrefabs.Count + NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount).First().Subscribe(_ =>
            {
                SceneProcessor.ChangeState(SceneState.TransitionEnd);

                if(NetworkingLocalData.PlayerNumber == 1)
                {
                    this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Exist).Subscribe(_ =>
                        {
                            InGameServerData.Instance.RPC_StartTimer(GeneralDataBase.Data.DefaultTimeLimit);
                        }
                    );
                }
            }
        );
    }
}
