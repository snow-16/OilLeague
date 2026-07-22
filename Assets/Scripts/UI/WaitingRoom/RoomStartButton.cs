using UniRx;
using System.Linq;

/// <summary>
/// ゲームを開始するボタン
/// </summary>
public class RoomStartButton : BasicButton
{
    protected override void Awake()
    {
        base.Awake();

        //全員が陣営を選択してからボタンを押せるようにする
        //開始ボタンはホストしか押せない
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
                        AudioSystem.Instance.RPC_StopBGM();
                        await SceneProcessor.TransitionScene("InGame");
                    }
                );
            }
        );
    }
}
