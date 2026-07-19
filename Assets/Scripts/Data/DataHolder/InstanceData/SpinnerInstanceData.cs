using System.Linq;
using Fusion;
using UnityEngine;

/// <summary>
/// スピナー毎の固有のデータ
/// </summary>
public class SpinnerInstanceData : NetworkBehaviour
{
    /// <summary> スピナーの陣営 </summary>
    [Networked]
    public SpinnerType Type { get; private set; }
    /// <summary> スピナーの状態 </summary>
    [Networked]
    public SpinnerState State { get; private set; }
    /// <summary> スピナーの進行方向 </summary>
    [Networked]
    public Vector2 Forword { get; private set; }

    void Awake()
    {
        GetComponents<IWriteSpinnerInstance>().ToList().ForEach(writeable => writeable.GiveWriter(this));
    }

    /// <summary>
    /// 陣営を設定する
    /// </summary>
    /// <param name="type">陣営</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetType(SpinnerType type)
    {
        Type = type;
    }

    /// <summary>
    /// 状態を設定する
    /// </summary>
    /// <param name="state">状態</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetState(SpinnerState state)
    {
        State = state;
    }

    /// <summary>
    /// 進行方向を設定する
    /// </summary>
    /// <param name="forword">進行方向</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetForword(Vector2 forword)
    {
        Forword = forword;
    }
}
