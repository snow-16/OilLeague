using UniRx;
using System.Linq;

public class RoomStartButton : BasicButton
{
    protected override void Awake()
    {
        base.Awake();

        _canPush = false;

        this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Exist).First().Subscribe(_ =>
            {
                Observable.EveryUpdate().Where(_ => NetworkingLocalData.PlayerNumber == 1).Select(_ => RoomServerData.Instance.Players).TakeUntil(Observable.EveryUpdate().Where(_ => SceneProcessor.State != SceneState.Exist)).Subscribe(players =>
                    {
                        _canPush = !players.Take(NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount).Select(player => player.type).Contains(SpinnerType.None);
                    }
                );
            }
        );
    }

    protected override void ClickAction()
    {
        NetworkingProcessor.StartGame();
        this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Loading).Subscribe( _ =>
            {
                RoomServerData.Instance.RPC_SaveData();
                Observable.EveryUpdate().Where(_ => RoomServerData.Instance.SaveFinished == NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount).First().Subscribe(async _ => 
                    {
                        await SceneProcessor.TransitionScene("InGame");
                    }
                );
            }
        );
    }
}
