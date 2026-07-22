using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// InputListLocalDataの書き換えクラス
/// </summary>
public class SpinnerDataWriter
{
    /// <summary> データクラスのインスタンス </summary>
    public SpinnerLocalData Data { get; }

    private SpinnerDataWriter()
    {
        Data = SpinnerLocalData.Access(this);
    }

    /// <summary>
    /// インスタンスの取得。
    /// ゲームマネージャー・入力受け取りクラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static SpinnerDataWriter Access(object accessed)
    {
        if(accessed is IWriteSpinnerLocal || accessed is GameManager)
        {
            return new SpinnerDataWriter();
        }

        Debug.LogError($"{nameof(SpinnerDataWriter)}へのアクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// データの初期化
    /// </summary>
    public void Reset()
    {
        Data.SetType(SpinnerType.Red);
        
        Data.SetTorque(0);
        Data.SetChargeTorque(0);
        Data.SetTurnCount(0);
        Data.SetPosition(Vector2.zero);
        Data.SetForword(Vector2.zero);
        Data.SetState(SpinnerState.Stop);
    }

    /// <summary>
    /// スピナータイプ決定
    /// </summary>
    /// <param name="type">スピナータイプ</param>
    public void SetType(SpinnerType type)
    {
        Data.SetType(type);
    }

    /// <summary>
    /// トルクのチャージ
    /// </summary>
    public void ChargeTorque()
    {
        var chargedTorque = SpinnerLocalData.ChargeTorque + SpinnerParameterDataBase.Data.TorqueChargeSpeed;

        if(chargedTorque > SpinnerParameterDataBase.Data.TorqueCriticalLimit)
        {
            Stan();
        }
        else
        {
            Data.SetChargeTorque(chargedTorque);
        }
    }

    /// <summary>
    /// チャージトルクを保持トルクに移行
    /// </summary>
    public void TransferTorque()
    {
        if(SpinnerLocalData.ChargeTorque > SpinnerLocalData.Torque)
        {
            Data.SetTorque(Mathf.Min(SpinnerLocalData.ChargeTorque, SpinnerParameterDataBase.Data.MaxTorque));
        }

        Data.SetChargeTorque(0);
        Data.SetState(SpinnerState.Spin);
    }

    /// <summary>
    /// 急ブレーキ
    /// </summary>
    public void Brake()
    {
        Data.SetTorque(Mathf.Max(SpinnerLocalData.Torque - (SpinnerParameterDataBase.Data.MaxTorque * SpinnerParameterDataBase.Data.TorqueDampingBrakeRatio), 0));
        Data.SetState(SpinnerState.Brake);
    }

    /// <summary>
    /// スピン中の旋回
    /// </summary>
    public void Turn()
    {
        Data.SetTurnCount(SpinnerLocalData.TurnCount + 1);
        TransferTorque();
    }

    /// <summary>
    /// スピン中のトルク減衰
    /// </summary>
    public void DampingTorque()
    {
        Data.SetTorque(SpinnerLocalData.Torque - SpinnerParameterDataBase.Data.TorqueDampingSpin * (SpinnerLocalData.State == SpinnerState.Strike ? SpinnerParameterDataBase.Data.StrikeDampingMultiplier : 1));
        
        if(SpinnerLocalData.Torque < 1)
        {
            Stan();
        }
    }

    /// <summary>
    /// スピン停止
    /// </summary>
    public void Stop()
    {
        Data.SetTorque(0);
        Data.SetChargeTorque(0);
        Data.SetTurnCount(0);
        Data.SetState(SpinnerState.Stop);
    }

    /// <summary>
    /// 自身の位置を保存
    /// </summary>
    /// <param name="spinnerTransform">現在位置</param>
    public void SavePosition(Transform spinnerTransform)
    {
        Data.SetPosition(spinnerTransform.position);
    }

    /// <summary>
    /// 移動方向を更新
    /// </summary>
    /// <param name="vector">角度</param>
    public void UpdateForword(Vector2 vector)
    {
        Data.SetForword(vector);
    }

    /// <summary>
    /// スタン状態へ移行
    /// </summary>
    public void Stan()
    {
        Stop();
        Data.SetState(SpinnerState.Stan);
        GameManager.Instance.SetTimer(1, () => Data.SetState(SpinnerState.Stop));
    }

    /// <summary>
    /// ストライクされた処理
    /// </summary>
    /// <param name="strikeVector">飛ばされる方角</param>
    public void Strike(Vector2 strikeVector, SpinnerType strikeFrom)
    {
        Stop();
        Data.SetState(SpinnerState.Strike);
        Data.SetTorque(SpinnerParameterDataBase.Data.StrikePower);
        Data.SetStrikeFrom(strikeFrom);
        UpdateForword(strikeVector);
    }
}
