using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン状態を管理するクラス
/// </summary>
public class SceneProcessor : IWriteSingletonsLocal
{
    /// <summary> シーン状態 </summary>
    public static SceneState State { get; private set;}

    /// <summary>
    /// シーン状態の変更
    /// </summary>
    /// <param name="state">変更先の状態</param>
    public static void ChangeState(SceneState state)
    {
        State = state;
    }

    /// <summary>
    /// シーンを遷移させる
    /// </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    /// <returns></returns>
    public static async Task TransitionScene(string sceneName)
    {
        new SceneProcessor().ResetSingletons();
        await NetworkingLocalData.NetworkRunner.LoadScene(sceneName);
    }

    /// <summary>
    /// タイトルシーンへ遷移する
    /// </summary>
    public static void TransitionToTitle()
    {
        ChangeState(SceneState.TransitionStart);
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(_ =>
            {
                new SceneProcessor().ResetSingletons();
                SceneManager.LoadScene("Title");
            }
        );
    }

    /// <summary>
    /// ロビーシーンへ遷移する
    /// </summary>
    public static void TransitionToLobby()
    {
        ChangeState(SceneState.TransitionStart);
        AudioPlayer.Instance.StopBGM();
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(_ =>
            {
                new SceneProcessor().ResetSingletons();
                SceneManager.LoadScene("Lobby");
            }
        );
    }

    /// <summary>
    /// ウェイティングルームシーンへ遷移する
    /// </summary>
    public static void TransitionToRoom()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
        AudioSystem.Instance.RPC_StopBGM();
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(async _ =>
            {
                await TransitionScene("WaitingRoom");
            }
        );
    }

    /// <summary>
    /// インゲームシーンへ遷移する
    /// </summary>
    public static void TransitionToInGame()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
        AudioSystem.Instance.RPC_StopBGM();
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(async _ =>
            {
                await TransitionScene("InGame");
            }
        );
    }

    /// <summary>
    /// リザルトシーンへ遷移する
    /// </summary>
    public static void TransitionToResult()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
        AudioSystem.Instance.RPC_StopBGM();
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(async _ =>
            {
                await TransitionScene("Result");
            }
        );
    }

    /// <summary>
    /// 遷移開始演出を再生する
    /// </summary>
    public static void PlayStartTransition()
    {
        ChangeState(SceneState.TransitionStart);
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(_ =>
            {
                new SceneProcessor().ResetSingletons();
            }
        );
    }

    /// <summary>
    /// シーン固有のシングルトンオブジェクトのデータ保持を破棄する
    /// </summary>
    private void ResetSingletons()
    {
        SingletonsDataWriter.Access(this).Reset();
        PlayerExistServerData.ResetData();
    }
}
