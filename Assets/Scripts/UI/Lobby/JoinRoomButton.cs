using UnityEngine;
using TMPro;

/// <summary>
/// 部屋へ入室するボタン
/// </summary>
public class JoinRoomButton : BasicButton
{
    /// <summary> 部屋コード入力用フィールド </summary>
    [SerializeField]
    private TMP_InputField _codeInputField;

    protected override void ClickAction()
    {
        NetworkingProcessor.CreateRoom(_codeInputField.text);
        AudioPlayer.Instance.StopBGM();
    }
}
