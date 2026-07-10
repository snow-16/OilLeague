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
            if(Application.isMobilePlatform)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            
            DontDestroyOnLoad(gameObject);

            //データベースの初期化
            var dataBase = transform.GetChild(0);
            dataBase.GetComponent<GeneralDataBase>().SetData();
            dataBase.GetComponent<SpinnerParameterDataBase>().SetData();
            dataBase.GetComponent<SpinnerTypeDataBase>().SetData();

            //データホルダーの初期化
            InputListDataWriter.Access().Reset();
            SpinnerDataWriter.Access().Reset();
            NetworkingDataWriter.Access().Reset();

            //入力処理をストリームに設定
            SubscribeInputSystem.SubscribeInputs();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
