/// <summary>
/// 長押し検知を許可するインターフェース
/// </summary>
public interface IReceiveHold
{
    /// <summary>
    /// 長押しされたとき
    /// </summary>
    void OnHold();
}