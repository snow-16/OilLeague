using System.Collections.Generic;

public class PlayerSettingClientData
{
    public static List<RoomServerData.PlayerSettings> Players { get; private set; }

    public static void ReceiveFromServer(List<RoomServerData.PlayerSettings> players)
    {
        Players = new(players);
    }

    public static void DownPlayerNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber < NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount)
        {
            Players[leftedPlayerNumber - 1] = Players[leftedPlayerNumber];
            DownPlayerNumber(leftedPlayerNumber + 1);
        }
        else
        {
            Players[leftedPlayerNumber - 1] = new();
        }
    }
}
