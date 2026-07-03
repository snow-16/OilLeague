using UnityEngine;

/// <summary>
/// ゲームの初期化用クラス
/// </summary>
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    void Awake() {
        if(FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length == 1)
        {
            DontDestroyOnLoad(gameObject);

            //データベースの初期化
            var dataBase = transform.GetChild(0);
            dataBase.GetComponent<GeneralDataBase>().SetData();

            //データホルダーの初期化
            InputListDataWriter.Access().Reset();

            //入力処理をストリームに設定
            SubscribeInputSystem.SubscribeInputs();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
