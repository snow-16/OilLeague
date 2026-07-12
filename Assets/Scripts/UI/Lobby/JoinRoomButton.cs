public class JoinRoomButton : BasicButton
{
    protected override async void ClickAction()
    {
        await NetworkingProcessor.CreateRoom("AAAAAA");
    }
}
