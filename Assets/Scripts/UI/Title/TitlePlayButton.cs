public class TitlePlayButton : BasicButton
{
    protected override void ClickAction()
    {
        NetworkingProcessor.GetSessionList();
    }
}
