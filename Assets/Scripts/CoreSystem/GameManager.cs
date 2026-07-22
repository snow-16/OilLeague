using System;
using UniRx;
using UnityEngine;

/// <summary>
/// ゲームの初期化用クラス
/// </summary>
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    /// <summary> ゲームマネージャーのインスタンス </summary>
    public static GameManager Instance { get; private set; }

    void Awake() {
        if(FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            //データベースの初期化
            var dataBase = transform.GetChild(0);
            dataBase.GetComponent<GeneralDataBase>().SetData();
            dataBase.GetComponent<SpinnerParameterDataBase>().SetData();
            dataBase.GetComponent<SpinnerTypeDataBase>().SetData();
            dataBase.GetComponent<OilmateTypeDataBase>().SetData();
            var resourceDataBase = transform.GetChild(1);
            resourceDataBase.GetComponent<AudioDataBase>().SetData();

            //データホルダーの初期化
            InputListDataWriter.Access(this).Reset();
            SpinnerDataWriter.Access(this).Reset();
            NetworkingDataWriter.Access(this).Reset();

            //入力処理をストリームに設定
            SubscribeInputSystem.SubscribeInputs();

            SceneProcessor.ChangeState(SceneState.Exist);
        }
        else
        {
            SceneProcessor.ChangeState(SceneState.TransitionEnd);
            Destroy(gameObject);
        }
    }

    public void SetTimer(float time, Action timeUpAction)
    {
        Observable.Timer(TimeSpan.FromSeconds(time), Scheduler.MainThreadIgnoreTimeScale).Subscribe(_ => timeUpAction()).AddTo(this);
    }
}
