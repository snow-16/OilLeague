public class JoinRoomButton : BasicButton
{
    protected override void ClickAction()
    {
        NetworkingProcessor.CreateRoom("AAAAAA");
    }
}
