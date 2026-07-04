using UnityEngine;

/// <summary>
/// 押下検知を許可するインターフェース
/// </summary>
public interface IReceivePress
{
    /// <summary>
    /// 押下されたとき
    /// </summary>
    /// <param name="pointerMoveVector">押された位置</param>
    void OnPress(Vector2 pointerMoveVector);
}