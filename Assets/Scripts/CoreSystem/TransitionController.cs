using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーン遷移演出を管理するクラス
/// </summary>
public class TransitionController : MonoBehaviour
{
    /// <summary> 暗転用UIとテキストUIを含む親オブジェクト </summary>
    private GameObject _transitionUI;
    /// <summary> 暗転用UI </summary>
    private Image _blackOutCircle;
    /// <summary> ローディング表示テキストUI </summary>
    private GameObject _loadingText;
    /// <summary> 画面上端の位置を示すトランスフォーム </summary>
    private Transform _underPosition;
    /// <summary> 画面下端の位置を示すトランスフォーム </summary>
    private Transform _topPosition;

    void Awake()
    {
        _transitionUI = transform.GetChild(0).gameObject;
        _blackOutCircle = _transitionUI.transform.GetChild(1).GetComponent<Image>();
        _loadingText = _transitionUI.transform.GetChild(2).gameObject;
        _underPosition = transform.GetChild(1);
        _topPosition = transform.GetChild(2);
        DontDestroyOnLoad(gameObject);

        //シーンが遷移開始・終了状態になったら対応する演出を再生する
        this.ObserveEveryValueChanged(_ => SceneProcessor.State).Subscribe(state =>
            {
                if(state == SceneState.TransitionStart)
                {
                    StartTransition(_underPosition.position, Vector2.zero, 0.5f, () => _blackOutCircle.fillAmount >= 1, 1, SceneState.Loading);
                }
                else if(state == SceneState.TransitionEnd)
                {
                    StartTransition(_topPosition.position, new Vector3(0, 0, 180), 1, () => _blackOutCircle.fillAmount <= 0.5f, -1, SceneState.Exist);
                }
            }
        );
    }

    /// <summary>
    /// シーン遷移演出を再生する
    /// </summary>
    /// <param name="centorPosition">暗転用オブジェクトの中心座標</param>
    /// <param name="angle">暗転用オブジェクトの角度</param>
    /// <param name="startFillAmount">開始時点での暗転率</param>
    /// <param name="transitionEndChecker">演出終了を検知する条件</param>
    /// <param name="fillOrDig">暗転させるか明転させるか</param>
    /// <param name="resultState">演出終了後のシーン状態</param>
    private void StartTransition(Vector2 centorPosition, Vector3 angle, float startFillAmount, Func<bool> transitionEndChecker, int fillOrDig, SceneState resultState)
    {
        _transitionUI.SetActive(true);
        _loadingText.SetActive(false);
        _blackOutCircle.transform.position = centorPosition;
        _blackOutCircle.transform.eulerAngles = angle;
        _blackOutCircle.fillAmount = startFillAmount;

        var transitionEnd = Observable.EveryUpdate().Where(_ => transitionEndChecker());
        Observable.EveryFixedUpdate().TakeUntil(transitionEnd).Subscribe(_ =>
            {
                _blackOutCircle.fillAmount += GeneralDataBase.Data.SceneTransitionSpeed * fillOrDig;
            }
        );
        transitionEnd.First().Subscribe(_ =>
            {
                SceneProcessor.ChangeState(resultState);

                if(resultState == SceneState.Exist)
                {
                    _transitionUI.SetActive(false);
                }
                else
                {
                    _loadingText.SetActive(true);
                }
            }
        );
    }
}
