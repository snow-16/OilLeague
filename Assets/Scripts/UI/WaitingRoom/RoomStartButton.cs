using UniRx;
using System.Linq;

public class RoomStartButton : BasicButton
{
    protected override void Awake()
    {
        base.Awake();

        _canPush = false;

        if(NetworkingLocalData.PlayerNumber == 1)
        {
            this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Exist).First().Subscribe(_ =>
                {
                    Observable.EveryUpdate().Select(_ => RoomServerData.Instance.Players).TakeUntil(Observable.EveryUpdate().Where(_ => SceneProcessor.State != SceneState.Exist)).Subscribe(players =>
                        {
                            _canPush = !players.Take(NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount).Select(player => player.type).Contains(SpinnerType.None);
                        }
                    );
                }
            );
        }
    }

    protected override void ClickAction()
    {
        NetworkingProcessor.StartGame();
        this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Loading).Subscribe( _ =>
            {
                RoomServerData.Instance.RPC_SaveData();
                this.ObserveEveryValueChanged(_ => RoomServerData.Instance).Subscribe(async _ => 
                    {
                        await SceneProcessor.TransitionScene("InGame");
                    }
                );
            }
        );
    }
}
