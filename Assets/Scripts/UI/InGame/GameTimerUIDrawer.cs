using UnityEngine;
using TMPro;

/// <summary>
/// 制限時間表示UI
/// </summary>
public class GameTimerUIDrawer : MonoBehaviour
{
    /// <summary> 時間表示テキスト </summary>
    private TextMeshProUGUI _remainingTimeText;

    void Awake()
    {
        _remainingTimeText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if(SceneProcessor.State == SceneState.Exist)
        {
            _remainingTimeText.text = InGameServerData.Instance.ProgressTime.ToString("000.00");
        }
    }
}
