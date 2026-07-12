using System.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

public class SceneProcessor
{
    public static SceneState State { get; private set;}

    public static void ChangeState(SceneState state)
    {
        State = state;
    }

    private static async Task TransitionScene(string sceneName)
    {
        await NetworkingLocalData.NetworkRunner.LoadScene(sceneName);
    }

    public static void TransitionToLobby()
    {
        ChangeState(SceneState.TransitionStart);
        Observable.EveryUpdate().Where(_ => State == SceneState.Loading).First().Subscribe(_ =>
            {
                SceneManager.LoadScene("Lobby");
            }
        );
    }

    public static async Task TransitionToRoom()
    {
        await TransitionScene("WaitingRoom");
    }

    public static async Task TransitionToInGame()
    {
        await TransitionScene("InGame");
    }

    public static async Task TransitionToResult()
    {
        await TransitionScene("Result");
    }
}
