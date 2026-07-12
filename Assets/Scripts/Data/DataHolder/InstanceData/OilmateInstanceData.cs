using System.Linq;
using Fusion;

public class OilmateInstanceData : NetworkBehaviour
{
    [Networked]
    public OilmateType Level { get; private set; }
    [Networked]
    public SpinnerType Parent { get; private set; }

    void Awake()
    {
        GetComponents<IWriteOilmateInstance>().ToList().ForEach(writeable => writeable.GiveWriter(this));
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetLevel(OilmateType level)
    {
        Level = level;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetParent(SpinnerType type)
    {
        Parent = type;
    }
}
