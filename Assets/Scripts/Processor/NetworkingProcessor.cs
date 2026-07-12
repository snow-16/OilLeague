using System.Linq;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkingProcessor : IWriteNetworkingLocal, IWriteSpinnerLocal
{
    public static void CreateNetworkRunner()
    {
        if(NetworkingLocalData.NetworkRunner != null)
        {
            ObjectSpawner.Instance.DestroySpawned(NetworkingLocalData.NetworkRunner.gameObject);
        }

        new NetworkingProcessor().SetRunner();
    }

    private static async Task StartSession(string sessionCode)
    {
        await NetworkingLocalData.NetworkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionCode,
                PlayerCount = 6,
                Scene = SceneRef.FromIndex(SceneManager.GetSceneByName("Lobby").buildIndex)
            }
        );
    }

    public static async Task GetSessionList()
    {
        CreateNetworkRunner();
        await NetworkingLocalData.NetworkRunner.JoinSessionLobby(SessionLobby.Shared);
        SceneManager.LoadScene("Lobby");
    }

    public static async Task JoinLobby()
    {
        if(NetworkingLocalData.AllSessions.Sum(session => session.PlayerCount) > GeneralDataBase.Data.MaxConnectablePlayerCount)
        {
            await NetworkingLocalData.NetworkRunner.Shutdown();
            return;
        }

        await StartSession("Lobby");
    }

    public static async Task CreateRoom(string sessionCode)
    {
        await NetworkingLocalData.NetworkRunner.Shutdown();
        CreateNetworkRunner();  
        await StartSession($"Close:{sessionCode}:NewRoom:Wait");
        new NetworkingProcessor().SetPlayerNumber();
        if(NetworkingLocalData.PlayerNumber == 1)
        {
            await NetworkingLocalData.NetworkRunner.LoadScene("InGame");
        }
    }

    public static async Task SpawnObject(GameObject prefab)
    {
        await NetworkingLocalData.NetworkRunner.SpawnAsync(prefab);
    }

    public static async Task SpawnObject(GameObject prefab, NetworkRunner.OnBeforeSpawned initialSetting)
    {
        await NetworkingLocalData.NetworkRunner.SpawnAsync(prefab, prefab.transform.position, prefab.transform.rotation, PlayerRef.None, initialSetting);
    }

    private void SetRunner()
    {
        NetworkingDataWriter.Access(this).Data.SetRunner(ObjectSpawner.Instance.SpawnDontDestroy(GeneralDataBase.Data.NetworkRunnerPrefab).GetComponent<NetworkRunner>());
    }

    private void SetPlayerNumber()
    {
        NetworkingDataWriter.Access(this).AssignPlayerNumber();
        SpinnerDataWriter.Access(this).SetType((SpinnerType)NetworkingLocalData.PlayerNumber);
    }
}
