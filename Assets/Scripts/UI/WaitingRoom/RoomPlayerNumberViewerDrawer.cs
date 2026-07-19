using UnityEngine;
using TMPro;

/// <summary>
/// プレイヤー番号表示UI
/// </summary>
public class RoomPlayerNumberViewerDrawer : MonoBehaviour
{
    /// <summary> プレイヤー番号表記テキスト </summary>
    private TextMeshProUGUI _playerNumberText;

    void Awake()
    {
        _playerNumberText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(SceneProcessor.State == SceneState.Exist)
        {
            _playerNumberText.text = $"Player {NetworkingLocalData.PlayerNumber}";
        }
    }
}
