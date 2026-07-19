using UnityEngine;
using TMPro;

/// <summary>
/// 部屋コード表示UI
/// </summary>
public class RoomCodeViewerDrawer : MonoBehaviour
{
    /// <summary> コード表記テキスト </summary>
    private TextMeshProUGUI _roomCodeText;

    void Awake()
    {
        _roomCodeText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(SceneProcessor.State == SceneState.Exist)
        {
            _roomCodeText.text = $"RoomCode : {NetworkingLocalData.NetworkRunner.SessionInfo.Name}";
        }
    }
}
