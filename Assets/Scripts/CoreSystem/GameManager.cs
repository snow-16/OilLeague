using UnityEngine;

/// <summary>
/// ゲームの初期化用クラス
/// </summary>
[DefaultExecutionOrder(100)]
public class GameManager : MonoBehaviour
{
    void Awake() {
        if(FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length == 1)
        {
            DontDestroyOnLoad(gameObject);

            SubscribeInputSystem.SubscribeInputs();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
