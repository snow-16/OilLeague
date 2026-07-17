using System.Linq;
using Fusion;
using UniRx;

public class PlayerExistServerData : NetworkBehaviour
{
    public static PlayerExistServerData[] Players { get; private set; } = new PlayerExistServerData[5];

    public void SetNumber(int number)
    {
        Players[number - 1] = this;

        if(number == 2)
        {
            Observable.EveryUpdate().Where(_ => Players.Take(NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount).Contains(null))
            .TakeUntil(Observable.EveryUpdate().Where(_ => SceneProcessor.State != SceneState.Exist))
            .Subscribe(_ =>
                {
                    RPCSendSystem.Instance.Object.RequestStateAuthority();
                    RPCSendSystem.Instance.RPC_DownPlayerNumber();
                }
            );
        }
    }

    public static void ResetData()
    {
        Players = new PlayerExistServerData[5];
    }

    public static void DownNumber(int leftedPlayerNumber)
    {
        if(leftedPlayerNumber < Players.Length)
        {
            Players[leftedPlayerNumber - 1] = Players[leftedPlayerNumber];
            DownNumber(leftedPlayerNumber + 1);
        }
        else
        {
            Players[leftedPlayerNumber - 1] = null;
        }
    }
}
