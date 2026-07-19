using System.Linq;
using Fusion;

/// <summary>
/// オイルメイト毎に固有のデータ
/// </summary>
public class OilmateInstanceData : NetworkBehaviour
{
    /// <summary> オイルメイトの成長段階 </summary>
    [Networked]
    public OilmateType Level { get; private set; }
    /// <summary> オイルメイトの陣営 </summary>
    [Networked]
    public SpinnerType Parent { get; private set; }

    void Awake()
    {
        GetComponents<IWriteOilmateInstance>().ToList().ForEach(writeable => writeable.GiveWriter(this));
    }

    /// <summary>
    /// 成長段階を設定する
    /// </summary>
    /// <param name="level">成長段階</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetLevel(OilmateType level)
    {
        Level = level;
    }

    /// <summary>
    /// 陣営を設定する
    /// </summary>
    /// <param name="type">陣営</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetParent(SpinnerType type)
    {
        Parent = type;
    }
}
