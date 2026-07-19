/// <summary>
/// スピナーインスタンスデータへのアクセスを許可するインターフェース
/// </summary>
public interface IWriteOilmateInstance
{
    /// <summary>
    /// インスタンスデータのインスタンスを渡す
    /// </summary>
    /// <param name="writer">データのインスタンス</param>
    void GiveWriter(OilmateInstanceData writer);
}