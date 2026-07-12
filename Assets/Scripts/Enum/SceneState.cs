/// <summary>
/// シーンの状態
/// </summary>
public enum SceneState
{
    /// <summary> ロード完了、ゲーム進行準備が整っている状態 </summary>
    Exist,
    /// <summary> ロード前の遷移状態、入力受付は停止される </summary>
    TransitionStart,
    /// <summary> ロード後、暗転解除中 </summary>
    TransitionEnd,
    /// <summary> ロード中 </summary>
    Loading
}
