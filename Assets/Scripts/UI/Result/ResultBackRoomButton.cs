public class ResultBackRoomButton : BasicButton
{
    protected override async void ClickAction()
    {
        Destroy(gameObject);
        await SceneProcessor.TransitionToRoom();
    }
}
