using System.Linq;
using Fusion;
using UniRx;

public class PlayerExistServerData : NetworkBehaviour
{
    public static PlayerExistServerData[] Players { get; private set; } = new PlayerExistServerData[5];

    [Networked]
    public int Number { get; private set; }

    public override void Spawned()
    {
        Players[Number - 1] = this;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {  
        if(NetworkingLocalData.NetworkRunner.IsInSession && SceneProcessor.State == SceneState.Exist && NetworkingLocalData.PlayerNumber == (Number > 1 ? 1 : 2))
        {
            Players[Number > 1 ? 0 : 1].RPC_DownPlayerNumber(Number);
        }
    }

    public void SetNumber(int number)
    {
        Number = number;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        if(!RPCSendSystem.Instance.Object.HasStateAuthority)
        {
            RPCSendSystem.Instance.Object.RequestStateAuthority();
        }

        Observable.EveryUpdate().Where(_ => RPCSendSystem.Instance.Object.HasStateAuthority).First().Subscribe(_ =>
            {
                RPCSendSystem.Instance.RPC_DownPlayerNumber(leftedPlayerNumber);
            }
        );
    }

    public static void ResetData()
    {
        Players = new PlayerExistServerData[5];
    }

    public static void DownNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber < Players.Length)
        {
            Players[leftedPlayerNumber - 1] = Players[leftedPlayerNumber];
            DownNumber(leftedPlayerNumber + 1);
        }
        else
        {
            Players[leftedPlayerNumber - 1] = null;
        }
    }
}
