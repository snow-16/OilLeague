using UnityEngine;
using TMPro;

public class RoomCodeViewerDrawer : MonoBehaviour
{
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
