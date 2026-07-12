using Fusion;
using UnityEngine;

public class SpinnerDamager : NetworkBehaviour, IWriteSpinnerLocal, IDamageable
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;
    /// <summary> スピナーインスタンスデータ閲覧用 </summary>
    private SpinnerInstanceData _spinnerInstanceData;

    void Awake()
    {
        _spinnerDataWriter = SpinnerDataWriter.Access(this);
        _spinnerInstanceData = GetComponent<SpinnerInstanceData>();
    }

    public SpinnerType GetCamp() => _spinnerInstanceData.Type;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_ReceiveDamage(SpinnerType attackSource, float damage, Vector2 attackerPosition, float pushPower)
    {
        if(SpinnerLocalData.State == SpinnerState.Stan || SpinnerLocalData.State == SpinnerState.Stop)
        {
            _spinnerDataWriter.Strike(((Vector2)transform.position - attackerPosition).normalized);
        }
    }
}
