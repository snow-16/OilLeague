using UniRx;

public class RoomStartButton : BasicButton
{
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
