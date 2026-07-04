using UnityEngine;

/// <summary>
/// フリック検知を許可するインターフェース
/// </summary>
public interface IReceiveFlick
{
    /// <summary>
    /// フリックを検知したとき
    /// </summary>
    /// <param name="pointerMoveVector">フリック方向のベクトル</param>
    void OnFlick(Vector2 pointerMoveVector);
}