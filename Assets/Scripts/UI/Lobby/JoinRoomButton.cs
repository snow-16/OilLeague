public class JoinRoomButton : BasicButton
{
    protected override async void ClickAction()
    {
        Destroy(gameObject);
        await NetworkingProcessor.CreateRoom("AAAAAA");
    }
}
