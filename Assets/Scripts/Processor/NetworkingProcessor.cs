using System.Threading.Tasks;
using Fusion;
using UnityEngine.SceneManagement;

public class NetworkingProcessor
{
    public static async Task StartSession(string sessionCode)
    {
        await NetworkingLocalData.NetworkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionCode,
                PlayerCount = 6,
                Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex)
            }
        );
    }
}
