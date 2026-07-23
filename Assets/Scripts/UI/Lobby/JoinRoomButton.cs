using UnityEngine;
using TMPro;
using UniRx;

/// <summary>
/// 部屋へ入室するボタン
/// </summary>
public class JoinRoomButton : BasicButton
{
    /// <summary> 部屋コード入力用フィールド </summary>
    [SerializeField]
    private TMP_InputField _codeInputField;

    protected override void Awake()
    {
        base.Awake();

        //コードが未入力ならボタンを押せないように
        _canPush = false;
        this.ObserveEveryValueChanged(_ => _codeInputField.text.Length).Subscribe(length => _canPush = length > 0);
    }

    protected override void ClickAction()
    {
        NetworkingProcessor.CreateRoom(_codeInputField.text);
        AudioPlayer.Instance.StopBGM();
    }
}
