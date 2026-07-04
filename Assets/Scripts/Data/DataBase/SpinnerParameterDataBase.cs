using UnityEngine;

/// <summary>
/// システム関連の定数データベース
/// </summary>
public class SpinnerParameterDataBase : MonoBehaviour
{
    /// <summary> 保持トルクの最大値 </summary>
    [SerializeField]
    private int _maxTorque;
    /// <summary> チャージトルクの最大値 </summary>
    [SerializeField]
    private int _torqueCriticalLimit;
    /// <summary> スピン中のトルク減衰量 </summary>
    [SerializeField]
    private float _torqueDampingSpin;
    /// <summary> ブレーキ時のトルク減少率 </summary>
    [SerializeField]
    private float _torqueDampingBrake;
    /// <summary> トルクのチャージ速度 </summary>
    [SerializeField]
    private float _torqueChargeSpeed;

    /// <summary> 移動速度の基底値 </summary>
    [SerializeField]
    private float _baseSpeed;
    /// <summary> トルクの移動速度倍率 </summary>
    [SerializeField]
    private float _speedTorqueMultiplier;
    /// <summary> ブレーキ中の速度定数 </summary>
    [SerializeField]
    private float _speedInBrake;
    /// <summary> 不完全ターンができる時間制限 </summary>
    [SerializeField]
    private float _quickTurnTimeLimit;

    /// <summary> 攻撃力の基底値 </summary>
    [SerializeField]
    private float _baseAttack;
    /// <summary> トルクの攻撃力倍率 </summary>
    [SerializeField]
    private float _attackTorqueMultiplier;
    /// <summary> ストライク時に与える初期速度 </summary>
    [SerializeField]
    private float _strikePower;

    /// <summary> 主衝撃波の基底距離 </summary>
    [SerializeField]
    private float _baseImpact;
    /// <summary> トルクの主衝撃波倍率 </summary>
    [SerializeField]
    private float _impactTorqueMultiplier;
    /// <summary> 主衝撃波の弧の長さ </summary>
    [SerializeField]
    private float _impactArc;
    /// <summary> 旋回時の衝撃波威力 </summary>
    [SerializeField]
    private float _turnImpactPower;
    /// <summary> 衝撃波用レイの射出数 </summary>
    [SerializeField]
    private float _impactRayCount;

    /// <summary> スタン時間 </summary>
    [SerializeField]
    private float _stanTime;


    /// <summary> ゲームフィールドの半径 </summary>
    public int MaxTorque { get => _maxTorque; }
    /// <summary> チャージトルクの最大値 </summary>
    public int TorqueCriticalLimit { get => _torqueCriticalLimit; }
    /// <summary> スピン中のトルク減衰量 </summary>
    public float TorqueDampingSpin { get => _torqueDampingSpin; }
    /// <summary> ブレーキ時のトルク減少率 </summary>
    public float TorqueDampingBrake { get => _torqueDampingBrake; }
    /// <summary> トルクのチャージ速度 </summary>
    public float TorqueChargeSpeed { get => _torqueChargeSpeed; }

    /// <summary> 移動速度の基底値 </summary>
    public float BaseSpeed { get => _baseSpeed; }
    /// <summary> トルクの移動速度倍率 </summary>
    public float SpeedTorqueMultiplier { get => _speedTorqueMultiplier; }
    /// <summary> ブレーキ中の速度定数 </summary>
    public float SpeedInBrake { get => _speedInBrake; }
    /// <summary> 不完全ターンができる時間制限 </summary>
    public float QuickTurnTimeLimit { get => _quickTurnTimeLimit; }

    /// <summary> 攻撃力の基底値 </summary>
    public float BaseAttack { get => _baseAttack; }
    /// <summary> トルクの攻撃力倍率 </summary>
    public float AttackTorqueMultiplier { get => _attackTorqueMultiplier; }
    /// <summary> ストライク時に与える初期速度 </summary>
    public float StrikePower { get => _strikePower; }

    /// <summary> 主衝撃波の基底距離 </summary>
    public float BaseImpact { get => _baseImpact; }
    /// <summary> トルクの主衝撃波倍率 </summary>
    public float ImpactTorqueMultiplier { get => _impactTorqueMultiplier; }
    /// <summary> 主衝撃波の弧の長さ </summary>
    public float ImpactArc { get => _impactArc; }
    /// <summary> 旋回時の衝撃波威力 </summary>
    public float TurnImpactPower { get => _turnImpactPower; }
    /// <summary> 衝撃波用レイの射出数 </summary>
    public float ImpactRayCount { get => _impactRayCount; }

    /// <summary> スタン時間 </summary>
    public float StanTime { get => _stanTime; }

    /// <summary> データのインスタンス </summary>
    public static SpinnerParameterDataBase Data { get; private set; }

    public void SetData()
    {
        Data = this;
    }
}
