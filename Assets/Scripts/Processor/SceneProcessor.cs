using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

public class SceneProcessor : IWriteSingletonsLocal
{
    public static SceneState State { get; private set;}

    public static void ChangeState(SceneState state)
    {
        State = state;
    }

    public static async Task TransitionScene(string sceneName)
    {
        new SceneProcessor().ResetSingletons();
        await NetworkingLocalData.NetworkRunner.LoadScene(sceneName);
    }

    public static void TransitionToLobby()
    {
        ChangeState(SceneState.TransitionStart);
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(_ =>
            {
                new SceneProcessor().ResetSingletons();
                SceneManager.LoadScene("Lobby");
            }
        );
    }

    public static void TransitionToRoom()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(async _ =>
            {
                await TransitionScene("WaitingRoom");
            }
        );
    }

    public static void TransitionToInGame()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(async _ =>
            {
                await TransitionScene("InGame");
            }
        );
    }

    public static void TransitionToResult()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(async _ =>
            {
                await TransitionScene("Result");
            }
        );
    }

    public static void PlayTransition(Action endAcion)
    {
        ChangeState(SceneState.TransitionStart);
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(_ =>
            {
                new SceneProcessor().ResetSingletons();
                endAcion();
            }
        );
    }

    private void ResetSingletons()
    {
        SingletonsDataWriter.Access(this).Reset();
        PlayerExistServerData.ResetData();
    }
}
