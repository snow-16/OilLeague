using System.Linq;
using System.Threading.Tasks;
using Fusion;
using UniRx;
using UnityEngine;

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
                PlayerCount = playerCount
            }
        );
    }

    public static void GetSessionList()
    {
        SceneProcessor.TransitionToLobby();
        Observable.EveryUpdate().Where(_ => SceneProcessor.State == SceneState.Loading).First().Subscribe(async _ =>
            {
                CreateNetworkRunner();
                await NetworkingLocalData.NetworkRunner.JoinSessionLobby(SessionLobby.Shared);
            }
        );
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

    public static void CreateRoom(string sessionCode)
    {
        SceneProcessor.ChangeState(SceneState.TransitionStart);
        Observable.EveryUpdate().Where(_ => SceneProcessor.State == SceneState.Loading).First().Subscribe(async _ =>
            {
                await NetworkingLocalData.NetworkRunner.Shutdown();
                CreateNetworkRunner();  
                await StartSession($"Close:{sessionCode}:NewRoom:Wait", GeneralDataBase.Data.RoomCapacity);
                new NetworkingProcessor().SetPlayerNumber();
                await SceneProcessor.TransitionScene("WaitingRoom");
            }
        );
    }

    public static void StartGame()
    {
        RPCSendSystem.Instance.RPC_PlayTransition();
    }

    public static async Task SpawnObject(GameObject prefab)
    {
        await NetworkingLocalData.NetworkRunner.SpawnAsync(prefab);
    }

    public static async Task SpawnObject(GameObject prefab, NetworkRunner.OnBeforeSpawned initialSetting)
    {
        await NetworkingLocalData.NetworkRunner.SpawnAsync(prefab, prefab.transform.position, prefab.transform.rotation, PlayerRef.None, initialSetting);
    }

    public static async Task SpawnObjectAtPosition(GameObject prefab, Vector2 position, NetworkRunner.OnBeforeSpawned initialSetting)
    {
        await NetworkingLocalData.NetworkRunner.SpawnAsync(prefab, position, prefab.transform.rotation, PlayerRef.None, initialSetting);
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
