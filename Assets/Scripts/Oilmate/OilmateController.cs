using UnityEngine;

/// <summary>
/// オイルメイト管理クラス
/// </summary>
public class OilmateController : MonoBehaviour, IDamageable
{
    /// <summary> 成長段階 </summary>
    private OilmateType _oilmateType;
    /// <summary> 生成元のスピナー </summary>
    [SerializeField]
    private SpinnerType _parentSpinner;

    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="type">成長段階</param>
    /// <param name="parent">生成元のスピナー</param>
    public void SetSettings(OilmateType type, SpinnerType parent)
    {
        _oilmateType = type;
        _parentSpinner = parent;
    }

    public SpinnerType GetCamp() => _parentSpinner;

    public void ReceiveDamage(float damage, Vector2 attackerPosition, float pushPower)
    {
        
    }
}
