using UniRx;

public class RoomStartButton : BasicButton
{
    protected override void ClickAction()
    {
        RoomServerData.Instance.RPC_SaveData();
        this.ObserveEveryValueChanged(_ => RoomServerData.Instance).Subscribe(async _ => 
            {
                await NetworkingProcessor.StartGame();
            }
        );
    }
}
