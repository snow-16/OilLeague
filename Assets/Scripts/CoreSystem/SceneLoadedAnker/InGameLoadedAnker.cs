using UniRx;
using UnityEngine;

/// <summary>
/// インゲームシーンが読み込まれたことを検知するクラス
/// </summary>
public class InGameLoadedAnker : SceneLoadedAnker
{
    protected override async void WhenLoaded()
    {
        //スピナーをフィールドの外周に等間隔でスポーンさせる
        await ObjectSpawner.Instance.SpawnNetworkAtPosition(SpinnerTypeDataBase.Data.SpinnerPrefab, Quaternion.Euler(0, 0, 360 / NetworkingLocalData.PlayerNumber / NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount) * Vector2.up * GeneralDataBase.Data.FieldRadius * 0.8f, (runner, obj) => obj.GetComponent<SpinnerInstanceData>().RPC_SetType(SpinnerLocalData.Type));

        SetGenerateEndTrigger(() => GeneratedCount == _networkedPrefabs.Count + NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount, () =>
            {
                AudioSystem.Instance.RPC_PlayBGM(AudioBGMType.Battle);
                StartTimer();
            }
        );
    }

    /// <summary>
    /// 暗転が明けた後、ホストがタイマーを開始させる
    /// </summary>
    private void StartTimer()
    {
        if(NetworkingLocalData.PlayerNumber == 1)
        {
            this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Exist).Subscribe(_ =>
                {
                    InGameServerData.Instance.RPC_StartTimer(PlayerSettingClientData.TimeLimit);
                }
            );
        }
    }
}
