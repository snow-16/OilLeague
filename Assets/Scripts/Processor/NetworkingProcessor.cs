using System.Linq;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkingProcessor : IWriteNetworkingLocal
{
    public static void CreateNetworkRunner()
    {
        if(NetworkingLocalData.NetworkRunner != null)
        {
            ObjectSpawner.Instance.DestroySpawned(NetworkingLocalData.NetworkRunner.gameObject);
        }

        new NetworkingProcessor().SetRunner();
    }

    private static async Task StartSession(string sessionCode, int playerCount)
    {
        await NetworkingLocalData.NetworkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionCode,
                PlayerCount = playerCount,
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

        await StartSession("Lobby", GeneralDataBase.Data.MaxConnectablePlayerCount);
    }

    public static async Task CreateRoom(string sessionCode)
    {
        await NetworkingLocalData.NetworkRunner.Shutdown();
        CreateNetworkRunner();  
        await StartSession($"Close:{sessionCode}:NewRoom:Wait", GeneralDataBase.Data.RoomCapacity);
        new NetworkingProcessor().SetPlayerNumber();
        if(NetworkingLocalData.PlayerNumber == 1)
        {
            await NetworkingLocalData.NetworkRunner.LoadScene("WaitingRoom");
        }
    }

    public static async Task StartGame()
    {
        await NetworkingLocalData.NetworkRunner.LoadScene("InGame");
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
    }
}
