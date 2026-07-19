using System.Collections.Generic;

/// <summary>
/// ゲーム終了後の各プレイヤーの取得オイル量データ
/// </summary>
public class OilResultClientData
{
    /// <summary> 各プレイヤーのオイル貯蔵量 </summary>
    public static List<InGameServerData.OilTank> Tanks { get; private set; }

    /// <summary>
    /// ネットワーク同期されたデータをローカルデータとして受け取る
    /// </summary>
    /// <param name="tanks">各プレイヤーのオイル量データ</param>
    public static void ReceiveFromServer(List<InGameServerData.OilTank> tanks)
    {
        Tanks = new(tanks);
    }
}
