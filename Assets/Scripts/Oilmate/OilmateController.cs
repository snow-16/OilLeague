using Fusion;
using UniRx;
using UnityEngine;

/// <summary>
/// オイルメイト管理クラス
/// </summary>
public class OilmateController : NetworkBehaviour, IDamageable, IWriteOilmateInstance
{
    /// <summary> インスタンスデータアクセス用 </summary>
    private OilmateInstanceData _oilmateInstanceData;

    public override void Spawned()
    {
        MapUIDrawer.CreateOilmateMarker(transform, _oilmateInstanceData.Parent);
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="type">成長段階</param>
    /// <param name="parent">生成元のスピナー</param>
    public void SetSettings(OilmateType level, SpinnerType parent)
    {
        this.ObserveEveryValueChanged(_ => _oilmateInstanceData).Where(data => data != null).First().Subscribe(data =>
            {
                data.RPC_SetLevel(level);
                data.RPC_SetParent(parent);
            }
        );
    }

    public SpinnerType GetCamp() => _oilmateInstanceData.Parent;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_ReceiveDamage(float damage, Vector2 attackerPosition, float pushPower)
    {
        Destroy(gameObject);
    }

    public void GiveWriter(OilmateInstanceData writer)
    {
        _oilmateInstanceData = writer;
    }
}
