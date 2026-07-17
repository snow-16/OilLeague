using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;

public class NetworkCallbacksReceiver : NetworkBehaviour, INetworkRunnerCallbacks
{
    private NetworkingDataWriter _networkingDataWriter;

    void Awake()
    {
        _networkingDataWriter = NetworkingDataWriter.Access(this);
    }

    public async void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        _networkingDataWriter.Data.SetAllSessions(sessionList);
        await NetworkingProcessor.JoinLobby();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        RPC_DownPlayerNumber();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_DownPlayerNumber()
    {
        if(NetworkingLocalData.PlayerNumber == 2)
        {
            SingletonsLocalData.Singletons.ForEach(singleton => singleton.Object.RequestStateAuthority());
            
            if(TryGetComponent<RoomServerData>(out var roomData))
            {
                roomData.RPC_DownPlayerNumber(NetworkingLocalData.PlayerNumber);
            }
            if(TryGetComponent<InGameServerData>(out var inGameData))
            {
                inGameData.RPC_DownPlayerNumber(NetworkingLocalData.PlayerNumber);
            }
            
            RPCSendSystem.Instance.RPC_DownPlayerNumber(NetworkingLocalData.PlayerNumber);
        }
    }

    //以下不要

    public void OnConnectedToServer(NetworkRunner runner){}

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason){}

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token){}

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data){}

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason){}

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken){}

    public void OnInput(NetworkRunner runner, NetworkInput input){}

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input){}

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){}

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){}

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player){}

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player){}

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress){}

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data){}

    public void OnSceneLoadDone(NetworkRunner runner){}

    public void OnSceneLoadStart(NetworkRunner runner){}

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message){}
}
