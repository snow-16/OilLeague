using System.Linq;
using Fusion;
using UniRx;

public class RoomServerData : NetworkBehaviour, IWriteSingletonsLocal
{
    public static RoomServerData Instance { get; private set; } = null;

    [Networked]
    public int SaveFinished { get; private set; }
    [Networked, Capacity(5)]
    public NetworkArray<PlayerSettings> Players => default;

    public override void Spawned()
    {
        Instance = this;
        SingletonsDataWriter.Access(this).Add(this);
        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_UpdateType(int playerNumber, SpinnerType type)
    {
        var copy = Players[playerNumber - 1];
        copy.type = type;
        Players.Set(playerNumber - 1, copy);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SaveData()
    {
        PlayerSettingClientData.ReceiveFromServer(Players.ToList());
        RPC_SaveFinished();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SaveFinished()
    {
        SaveFinished++;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber < NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount)
        {
            RPC_UpdateType(leftedPlayerNumber, Players[leftedPlayerNumber].type);
            RPC_DownPlayerNumber(leftedPlayerNumber + 1);
        }
        else
        {
            RPC_UpdateType(leftedPlayerNumber, SpinnerType.None);
        }
    }

    public struct PlayerSettings : INetworkStruct
    {
        public SpinnerType type;
    }
}
