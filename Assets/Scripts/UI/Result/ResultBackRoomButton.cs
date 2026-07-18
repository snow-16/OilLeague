using UniRx;

public class ResultBackRoomButton : BasicButton
{
    protected override void Awake()
    {
        base.Awake();

        if(NetworkingLocalData.PlayerNumber != 1)
        {
            _canPush = false;

            this.ObserveEveryValueChanged(_ => NetworkingLocalData.PlayerNumber).Where(number => number == 1).Subscribe(_ =>
                {
                    _canPush = true;
                }
            );
        }
    }
    
    protected override void ClickAction()
    {
        SceneProcessor.TransitionToRoom();
    }
}
