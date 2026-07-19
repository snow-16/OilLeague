using UnityEngine;
using TMPro;
using System.Linq;

/// <summary>
/// 陣営選択済みプレイヤーカウンターUI
/// </summary>
public class RoomSelectedCounterDrawer : MonoBehaviour
{
    /// <summary> カウンター表記テキスト </summary>
    private TextMeshProUGUI _counterText;

    void Awake()
    {
        _counterText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(SceneProcessor.State == SceneState.Exist)
        {
            var playerCount = NetworkingLocalData.NetworkRunner.SessionInfo.PlayerCount;
            var selectedCount = RoomServerData.Instance.Players.Select(players => players.type).Count(type => type != SpinnerType.None);
            _counterText.text = $"Selected: {selectedCount}/{playerCount}";
        }
    }
}
