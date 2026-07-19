using UniRx;

/// <summary>
/// ロビーシーンが読み込まれたことを検知するクラス
/// </summary>
public class LobbyLoadedAnker : SceneLoadedAnker
{
    protected override void WhenLoaded()
    {
        Observable.EveryUpdate().Where(_ => NetworkingLocalData.NetworkRunner.IsConnectedToServer).First().Subscribe(_ => SceneProcessor.ChangeState(SceneState.TransitionEnd));
    }
}
