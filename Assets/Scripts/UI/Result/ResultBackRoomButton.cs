public class ResultBackRoomButton : BasicButton
{
    protected override void ClickAction()
    {
        SceneProcessor.TransitionToRoom();
    }
}
