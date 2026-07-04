using UnityEngine;

/// <summary>
/// タップ検知を許可するインターフェース
/// </summary>
public interface IReceiveTap
{
    /// <summary>
    /// タップされたとき
    /// </summary>
    /// <param name="tapPosition">タップされた位置</param>
    void OnTap(Vector2 tapPosition);
}