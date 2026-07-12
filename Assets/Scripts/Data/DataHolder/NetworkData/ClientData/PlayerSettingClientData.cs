using System.Collections.Generic;

public class PlayerSettingClientData
{
    public static List<RoomServerData.PlayerSettings> Players { get; private set; }

    public static void ReceiveFromServer(List<RoomServerData.PlayerSettings> players)
    {
        Players = new(players);
    }
}
