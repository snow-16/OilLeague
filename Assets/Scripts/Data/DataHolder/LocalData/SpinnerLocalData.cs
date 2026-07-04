using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// 各種入力を受け付けるインスタンスのリストデータ
/// </summary>
public class SpinnerLocalData
{
    /// <summary> 自分のスピナータイプ </summary>
    private static SpinnerType _type;
    /// <summary> 自分のスピナータイプ </summary>
    public static SpinnerType Type => _type;

    /// <summary> 現在の保持トルク </summary>
    private static float _torque;
    /// <summary> 現在の保持トルク </summary>
    public static float Torque => _torque;

    /// <summary> チャージ中のトルク </summary>
    private static float _chargeTorque;
    /// <summary> チャージ中のトルク </summary>
    public static float ChargeTorque => _chargeTorque;

    /// <summary> スピン中の旋回回数 </summary>
    private static int _turnCount;
    /// <summary> スピン中の旋回回数 </summary>
    public static int TurnCount => _turnCount;

    /// <summary> 現在位置 </summary>
    private static Vector2 _position;
    /// <summary> 現在位置 </summary>
    public static Vector2 Position => _position;

    /// <summary>
    /// インスタンスの取得。
    /// ライタークラスからのみアクセス可能
    /// </summary>
    /// <returns>インスタンス</returns>
    public static SpinnerLocalData Access()
    {
        var accessedClass = new StackFrame(1).GetMethod()?.ReflectedType;
        if(accessedClass == typeof(SpinnerDataWriter))
        {
            return new SpinnerLocalData();
        }

        Debug.LogError("アクセス権限がありません。");
        return null;
    }

    /// <summary>
    /// スピナータイプ書き換え
    /// </summary>
    /// <param name="value">スピナータイプ</param>
    public void SetType(SpinnerType value)
    {
        _type = value;
    }

    /// <summary>
    /// 現在トルク書き換え
    /// </summary>
    /// <param name="value">トルク量</param>
    public void SetTorque(float value)
    {
        _torque = value;
    }

    /// <summary>
    /// チャージトルク書き換え
    /// </summary>
    /// <param name="value">トルク量</param>
    public void SetChargeTorque(float value)
    {
        _chargeTorque = value;
    }

    /// <summary>
    /// 旋回数書き換え
    /// </summary>
    /// <param name="value">旋回数</param>
    public void SetTurnCount(int value)
    {
        _turnCount = value;
    }

    /// <summary>
    /// 現在位置書き換え
    /// </summary>
    /// <param name="value">位置</param>
    public void SetPosition(Vector2 value)
    {
        _position = value;
    }
}
