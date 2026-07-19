using System.Linq;
using Fusion;
using UniRx;
using UnityEngine;

/// <summary>
/// インゲームシーン内でネットワーク同期するデータ
/// </summary>
public class InGameServerData : NetworkBehaviour, IWriteSingletonsLocal
{
    /// <summary> データのインスタンス </summary>
    public static InGameServerData Instance { get; private set; } = null;

    /// <summary> データの保存が完了したクライアントの数 </summary>
    [Networked]
    public int SaveFinished { get; private set; }
    /// <summary> 各プレイヤーのオイル貯蔵データ </summary>
    [Networked, Capacity(5)]
    public NetworkArray<OilTank> OilTanks => default;
    /// <summary> 経過時間 </summary>
    [Networked]
    public float ProgressTime { get; private set; }

    public override void Spawned()
    {
        Instance = this;
        SingletonsDataWriter.Access(this).Add(this);

        ///ホストがオイル貯蔵データの初期化を行う
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

    /// <summary>
    /// オイル獲得処理
    /// </summary>
    /// <param name="spinner">オイルを獲得した陣営</param>
    /// <param name="amount">獲得量</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_AddAmount(SpinnerType spinner, int amount)
    {
        var index = OilTanks.ToList().FindIndex(tank => tank.spinner == spinner);
        var copyFromTanks = OilTanks[index];
        copyFromTanks.oilAmount += amount;
        OilTanks.Set(index, copyFromTanks);
    }

    /// <summary>
    /// <para>時間経過を開始させる</para>
    /// <para>ネットワーク同期のためUniRxは不使用</para>
    /// </summary>
    /// <param name="timeLimit">今ゲームの制限時間</param>
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

    /// <summary>
    /// データをクライアント側で保存する
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SaveData()
    {
        OilResultClientData.ReceiveFromServer(OilTanks.ToList());
        RPC_SaveFinished();
    }

    /// <summary>
    /// 保存完了を通知する
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SaveFinished()
    {
        SaveFinished++;
    }

    /// <summary>
    /// プレイヤー退室時にデータを並べ替える
    /// </summary>
    /// <param name="leftedPlayerNumber">退室したプレイヤーの番号</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber < NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount)
        {
            OilTanks.Set(leftedPlayerNumber - 1, OilTanks[leftedPlayerNumber]);
            RPC_DownPlayerNumber(leftedPlayerNumber + 1);
        }
        else
        {
            OilTanks.Set(leftedPlayerNumber - 1, new());
        }
    }

    /// <summary>
    /// プレイヤーのオイル貯蔵データ
    /// </summary>
    public struct OilTank : INetworkStruct
    {
        /// <summary> 貯蔵量 </summary>
        public int oilAmount;
        /// <summary> 陣営 </summary>
        public SpinnerType spinner;
    }
}
