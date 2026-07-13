using UnityEngine;
using TMPro;

public class JoinRoomButton : BasicButton
{
    [SerializeField]
    private TMP_InputField _codeInputField;

    protected override void ClickAction()
    {
        NetworkingProcessor.CreateRoom(_codeInputField.text);
    }
}
