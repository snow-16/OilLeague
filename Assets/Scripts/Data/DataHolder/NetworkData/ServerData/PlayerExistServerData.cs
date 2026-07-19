using System.Linq;
using Fusion;
using UniRx;

/// <summary>
/// プレイヤー退室検知用クラス
/// </summary>
public class PlayerExistServerData : NetworkBehaviour
{
    /// <summary> シーン上に存在するプレイヤーのリスト </summary>
    public static PlayerExistServerData[] Players { get; private set; } = new PlayerExistServerData[5];

    /// <summary> 紐づけられたプレイヤーの番号 </summary>
    [Networked]
    public int Number { get; private set; }

    public override void Spawned()
    {
        Players[Number - 1] = this;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        //プレイヤーが退室したなら、退室処理を開始する
        //現在存在しているプレイヤーの中で最も番号が若いプレイヤーに処理させる
        if(NetworkingLocalData.NetworkRunner.IsInSession && SceneProcessor.State == SceneState.Exist && NetworkingLocalData.PlayerNumber == (Number > 1 ? 1 : 2))
        {
            Players[Number > 1 ? 0 : 1].RPC_DownPlayerNumber(Number);
        }
    }

    /// <summary>
    /// プレイヤーと紐付けを行う
    /// </summary>
    /// <param name="number">紐付けるプレイヤーの番号</param>
    public void SetNumber(int number)
    {
        Number = number;
    }

    /// <summary>
    /// <para>プレイヤー退室時ホストがデータ調整処理を行う</para>
    /// <para>ホストが退室した場合は、2番のプレイヤーをホストとして再設定する</para>
    /// </summary>
    /// <param name="leftedPlayerNumber">退室したプレイヤーの番号</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DownPlayerNumber(int leftedPlayerNumber)
    {
        if(!RPCSendSystem.Instance.Object.HasStateAuthority)
        {
            RPCSendSystem.Instance.Object.RequestStateAuthority();
            SingletonsLocalData.Singletons.ForEach(singleton => singleton.Object.RequestStateAuthority());
        }

        Observable.EveryUpdate().Where(_ => RPCSendSystem.Instance.Object.HasStateAuthority).First().Subscribe(_ =>
            {
                RPCSendSystem.Instance.RPC_DownPlayerNumber(leftedPlayerNumber);
            }
        );
    }

    /// <summary>
    /// シーン遷移時にリストを初期化する
    /// </summary>
    public static void ResetData()
    {
        Players = new PlayerExistServerData[5];
    }

    /// <summary>
    /// プレイヤー退室時にデータを並べ替える
    /// </summary>
    /// <param name="leftedPlayerNumber">退室したプレイヤーの番号</param>
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
