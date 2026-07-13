using UnityEngine;
using TMPro;

public class GameTimerUIDrawer : MonoBehaviour
{
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
