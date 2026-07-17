using UniRx;
using UnityEngine;


/// <summary>
/// ルームシーンが読み込まれたことを検知するクラス
/// </summary>
public class WaitingRoomLoadedAnker : SceneLoadedAnker
{
    [SerializeField]
    private GameObject _playerExistDataPrefab;
    
    protected override async void WhenLoaded()
    {
        await ObjectSpawner.Instance.SpawnNetwork(_playerExistDataPrefab, (runner, obj) => obj.GetComponent<PlayerExistServerData>().SetNumber(NetworkingLocalData.PlayerNumber));

        Observable.EveryUpdate().Where(_ => Generated == _networkedPrefabs.Count).First().Subscribe(_ =>
            {
                SceneProcessor.ChangeState(SceneState.TransitionEnd);
            }
        );
    }
}
