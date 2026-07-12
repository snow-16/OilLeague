using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// 各種入力を受け付けるインスタンスのリストデータ
/// </summary>
public class SpinnerLocalData
{
    /// <summary> 自分のスピナータイプ </summary>
    public static SpinnerType Type { get; private set; }

    /// <summary> 現在の自分の状態 </summary>
    public static SpinnerState State { get; private set; }

    /// <summary> 現在の保持トルク </summary>
    public static float Torque { get; private set; }

    /// <summary> チャージ中のトルク </summary>
    public static float ChargeTorque { get; private set; }

    /// <summary> スピン中の旋回回数 </summary>
    public static int TurnCount { get; private set; }

    /// <summary> 現在位置 </summary>
    public static Vector2 Position { get; private set; }

    /// <summary> 移動方向 </summary>
    public static Vector2 Forword { get; private set; }

    private SpinnerLocalData()
    {
        
    }

    /// <summary>
    /// インスタンスの取得。
    /// ライタークラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static SpinnerLocalData Access(object accessed)
    {
        if(accessed is SpinnerDataWriter)
        {
            return new SpinnerLocalData();
        }

        Debug.LogError($"{nameof(SpinnerLocalData)}へのアクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// スピナータイプ書き換え
    /// </summary>
    /// <param name="value">スピナータイプ</param>
    public void SetType(SpinnerType value)
    {
        Type = value;
    }

    /// <summary>
    /// 状態書き換え
    /// </summary>
    /// <param name="value">状態</param>
    public void SetState(SpinnerState value)
    {
        State = value;
    }

    /// <summary>
    /// 現在トルク書き換え
    /// </summary>
    /// <param name="value">トルク量</param>
    public void SetTorque(float value)
    {
        Torque = value;
    }

    /// <summary>
    /// チャージトルク書き換え
    /// </summary>
    /// <param name="value">トルク量</param>
    public void SetChargeTorque(float value)
    {
        ChargeTorque = value;
    }

    /// <summary>
    /// 旋回数書き換え
    /// </summary>
    /// <param name="value">旋回数</param>
    public void SetTurnCount(int value)
    {
        TurnCount = value;
    }

    /// <summary>
    /// 現在位置書き換え
    /// </summary>
    /// <param name="value">位置</param>
    public void SetPosition(Vector2 value)
    {
        Position = value;
    }

    /// <summary>
    /// 移動方向書き換え
    /// </summary>
    /// <param name="value">移動ベクトル</param>
    public void SetForword(Vector2 value)
    {
        Forword = value;
    }
}
