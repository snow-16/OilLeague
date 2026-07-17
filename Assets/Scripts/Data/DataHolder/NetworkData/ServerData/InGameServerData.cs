using System.Linq;
using Fusion;
using UniRx;
using UnityEngine;

public class InGameServerData : NetworkBehaviour
{
    public static InGameServerData Instance { get; private set; } = null;

    [Networked]
    public int SaveFinished { get; private set; }
    [Networked, Capacity(5)]
    public NetworkArray<OilTank> OilTanks => default;
    [Networked]
    public float ProgressTime { get; private set; }

    public override void Spawned()
    {
        Instance = this;

        if(NetworkingLocalData.PlayerNumber == 1)
        {
            for(int i = 0; i < NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount; i++)
            {
                var copy = OilTanks[i];
                copy.spinner = PlayerSettingClientData.Players[i].type;
                OilTanks.Set(i, copy);
            }
        }

        FindAnyObjectByType<SceneLoadedAnker>().OnGenerated();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_AddAmount(SpinnerType spinner, int amount)
    {
        var index = OilTanks.ToList().FindIndex(tank => tank.spinner == spinner);
        var copyFromTanks = OilTanks[index];
        copyFromTanks.oilAmount += amount;
        OilTanks.Set(index, copyFromTanks);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_StartTimer(int timeLimit)
    {
        ProgressTime = timeLimit;
        var catchTimeOver = Observable.EveryUpdate().Where(_ => ProgressTime <= 0);
        Observable.EveryUpdate().TakeUntil(catchTimeOver).Subscribe(_ =>
            {
                ProgressTime -= Time.deltaTime;
            }
        );
        catchTimeOver.First().Subscribe(_ =>
            {
                RPC_SaveData();
                Observable.EveryUpdate().Where(_ => SaveFinished == NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount).First().Subscribe(_ => 
                    {
                        SceneProcessor.TransitionToResult();
                    }
                );
            }
        );
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SaveData()
    {
        OilResultClientData.ReceiveFromServer(OilTanks.ToList());
        RPC_SaveFinished();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SaveFinished()
    {
        SaveFinished++;
    }

    public struct OilTank : INetworkStruct
    {
        public int oilAmount;
        public SpinnerType spinner;
    }
}
