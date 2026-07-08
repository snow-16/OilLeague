using Fusion;
using UnityEngine;

public class RunnerGenerater : MonoBehaviour, IWriteNetworkingLocal
{
    [SerializeField]
    private GameObject _networkRunnerPrefab;

    void Awake()
    {
        var networkRunnerInstance = Instantiate(_networkRunnerPrefab);
        DontDestroyOnLoad(networkRunnerInstance);
        NetworkingDataWriter.Access().Data.SetRunner(networkRunnerInstance.GetComponent<NetworkRunner>());

        StartGame();
    }

    private async void StartGame()
    {
        await NetworkingProcessor.StartSession("Lobby");
        NetworkingDataWriter.Access().Data.SetPlayerNumber(NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount);
    }
}
