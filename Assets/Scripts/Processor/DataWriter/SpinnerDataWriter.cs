using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// InputListLocalDataの書き換えクラス
/// </summary>
public class SpinnerDataWriter
{
    /// <summary> データクラスのインスタンス </summary>
    public SpinnerLocalData Data { get; }

    public SpinnerDataWriter()
    {
        Data = SpinnerLocalData.Access();
    }

    /// <summary>
    /// インスタンスの取得。
    /// ゲームマネージャー・入力受け取りクラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static SpinnerDataWriter Access()
    {
        var accessedClass = new StackFrame(1).GetMethod()?.ReflectedType;
        if(typeof(IWriteSpinnerLocal).IsAssignableFrom(accessedClass) || accessedClass == typeof(GameManager))
        {
            return new SpinnerDataWriter();
        }

        Debug.LogError("アクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// データの初期化
    /// </summary>
    public void Reset()
    {
        Data.SetTorque(0);
        Data.SetChargeTorque(0);
        Data.SetTurnCount(0);
        Data.SetPosition(Vector2.zero);
        Data.SetForword(0);
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
        Data.SetChargeTorque(Mathf.Min(SpinnerLocalData.ChargeTorque + SpinnerParameterDataBase.Data.TorqueChargeSpeed, SpinnerParameterDataBase.Data.TorqueCriticalLimit));
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
    }

    /// <summary>
    /// 急ブレーキ
    /// </summary>
    public void Brake()
    {
        Data.SetTorque(SpinnerLocalData.Torque * (1 - SpinnerParameterDataBase.Data.TorqueDampingBrake));
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
        Data.SetTorque(SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.TorqueDampingSpin);
        
        if(SpinnerLocalData.Torque < 0.1f)
        {
            Reset();
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
    }

    /// <summary>
    /// 自身のトランスフォームを保存
    /// </summary>
    /// <param name="spinnerTransform">自分のトランスフォーム</param>
    public void SaveTransform(Transform spinnerTransform)
    {
        Data.SetPosition(spinnerTransform.position);
        Data.SetForword(spinnerTransform.eulerAngles.z);
    }
}
