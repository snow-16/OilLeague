using UnityEngine;
using TMPro;

public class RoomTimeLimitViewUI : MonoBehaviour
{
    private TextMeshProUGUI _timeLimitText;

    void Awake()
    {
        _timeLimitText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(SceneProcessor.State == SceneState.Exist)
        {
            int timeLimit = RoomServerData.Instance.TimeLimit;
            int second = timeLimit % 60;
            _timeLimitText.text = $"{(timeLimit - second) / 60}m{second}s";
        }
    }
}
