using System.Threading.Tasks;

public class SceneProcessor
{
    private static async Task TransitionScene(string sceneName)
    {
        await NetworkingLocalData.NetworkRunner.LoadScene(sceneName);
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
