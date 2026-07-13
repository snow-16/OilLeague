public class ResultBackRoomButton : BasicButton
{
    protected override void Awake()
    {
        base.Awake();

        if(NetworkingLocalData.PlayerNumber != 1)
        {
            _canPush = false;
        }
    }
    
    protected override void ClickAction()
    {
        SceneProcessor.TransitionToRoom();
    }
}
