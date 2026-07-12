using System.Collections.Generic;

public class OilResultClientData
{
    public static List<InGameServerData.OilTank> Tanks { get; private set; }

    public static void ReceiveFromServer(List<InGameServerData.OilTank> tanks)
    {
        Tanks = new(tanks);
    }
}
