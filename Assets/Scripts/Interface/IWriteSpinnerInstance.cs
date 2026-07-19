/// <summary>
/// スピナーインスタンスデータへのアクセスを許可するインターフェース
/// </summary>
public interface IWriteSpinnerInstance
{
    /// <summary>
    /// インスタンスデータのインスタンスを渡す
    /// </summary>
    /// <param name="writer">データのインスタンス</param>
    void GiveWriter(SpinnerInstanceData writer);
}