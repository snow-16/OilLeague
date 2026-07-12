public class ResultBackRoomButton : BasicButton
{
    protected override async void ClickAction()
    {
        await SceneProcessor.TransitionToRoom();
    }
}
