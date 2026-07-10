public class TitlePlayButton : BasicButton
{
    protected override async void ClickAction()
    {
        Destroy(gameObject);
        await NetworkingProcessor.GetSessionList();
    }
}
