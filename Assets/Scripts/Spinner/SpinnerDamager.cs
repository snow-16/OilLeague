using UnityEngine;

public class SpinnerDamager : MonoBehaviour, IWriteSpinnerLocal, IDamageable
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;
    /// <summary> スピナーインスタンスデータ閲覧用 </summary>
    private SpinnerInstanceData _spinnerInstanceData;

    void Awake()
    {
        _spinnerDataWriter = SpinnerDataWriter.Access();
        _spinnerInstanceData = GetComponent<SpinnerInstanceData>();
    }

    public SpinnerType GetCamp() => _spinnerInstanceData.Type;

    public void ReceiveDamage(float damage, Vector2 attackerPosition, float pushPower)
    {
        if(SpinnerLocalData.State == SpinnerState.Stan || SpinnerLocalData.State == SpinnerState.Stop)
        {
            _spinnerDataWriter.Strike(((Vector2)transform.position - attackerPosition).normalized);
        }
    }
}
