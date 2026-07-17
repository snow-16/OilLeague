using UnityEngine;
using TMPro;

public class RoomPlayerNumberViewerDrawer : MonoBehaviour
{
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
