using System.Linq;
using Fusion;
using UnityEngine;

public class SpinnerInstanceData : NetworkBehaviour
{
    [Networked]
    public SpinnerType Type { get; private set; }
    [Networked]
    public SpinnerState State { get; private set; }
    [Networked]
    public Vector2 Forword { get; private set; }

    void Awake()
    {
        GetComponents<IWriteSpinnerInstance>().ToList().ForEach(writeable => writeable.GiveWriter(this));
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetType(SpinnerType type)
    {
        Type = type;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetState(SpinnerState type)
    {
        State = type;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetForword(Vector2 type)
    {
        Forword = type;
    }
}
