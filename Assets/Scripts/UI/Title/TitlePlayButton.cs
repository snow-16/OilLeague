/// <summary>
/// タイトルからロビーへ移動するボタン
/// </summary>
public class TitlePlayButton : BasicButton
{
    protected override void ClickAction()
    {
        NetworkingProcessor.GetSessionList();
    }
}
