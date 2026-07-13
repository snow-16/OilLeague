using UniRx;


/// <summary>
/// リザルトシーンが読み込まれたことを検知するクラス
/// </summary>
public class ResultLoadedAnker : SceneLoadedAnker
{
    protected override void WhenLoaded()
    {
        Observable.EveryUpdate().Where(_ => Generated == _networkedPrefabs.Count).First().Subscribe(_ =>
            {
                SceneProcessor.ChangeState(SceneState.TransitionEnd);
            }
        );
    }
}
