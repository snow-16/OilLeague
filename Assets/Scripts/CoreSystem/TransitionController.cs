using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーン遷移演出を管理するクラス
/// </summary>
public class TransitionController : MonoBehaviour
{
    private GameObject _transitionUI;
    private Image _blackOutCircle;
    private GameObject _loadingText;
    private Transform _underPosition;
    private Transform _topPosition;

    void Awake()
    {
        _transitionUI = transform.GetChild(0).gameObject;
        _blackOutCircle = _transitionUI.transform.GetChild(1).GetComponent<Image>();
        _loadingText = _transitionUI.transform.GetChild(2).gameObject;
        _underPosition = transform.GetChild(1);
        _topPosition = transform.GetChild(2);
        DontDestroyOnLoad(gameObject);

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
