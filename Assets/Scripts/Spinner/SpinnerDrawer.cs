using UniRx;
using UnityEngine;

/// <summary>
/// スピナーの描画関係を処理するクラス
/// </summary>
public class SpinnerDrawer : MonoBehaviour
{
    /// <summary> スピナーのスプライトレンダラ </summary>
    private SpriteRenderer _spinnerRenderer;
    /// <summary> スピン中スピナーのマフラーのスプライトレンダラ </summary>
    private SpriteRenderer _coreRenderer;
    /// <summary> スピナーインスタンスデータ閲覧用 </summary>
    private SpinnerInstanceData _spinnerInstanceData;

    void Awake()
    {
        _spinnerRenderer = GetComponent<SpriteRenderer>();
        _spinnerInstanceData = GetComponent<SpinnerInstanceData>();
        _coreRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(_spinnerInstanceData.Type == SpinnerLocalData.Type)
        {
            //回転
            if(SpinnerLocalData.Torque > 0 && SpinnerLocalData.State != SpinnerState.Brake)
            {
                if(_spinnerInstanceData.Type == SpinnerLocalData.Type)
                {
                    transform.eulerAngles += new Vector3(0, 0, SpinnerLocalData.Torque);
                }

                _coreRenderer.transform.eulerAngles = Vector3.zero;
            }
            else if(transform.eulerAngles != Vector3.zero)
            {
                if(_spinnerInstanceData.Type == SpinnerLocalData.Type)
                {
                    transform.eulerAngles = Vector3.zero;
                }
            }
        }

        //方向
        if(_spinnerInstanceData.Forword.x < 0)
        {
            _spinnerRenderer.flipX = false;
            _coreRenderer.flipX = false;
        }
        else
        {
            _spinnerRenderer.flipX = true;
            _coreRenderer.flipX = true;
        }

        this.ObserveEveryValueChanged(_ => _spinnerInstanceData.State).Subscribe(state =>
            {
                if(state == SpinnerState.Strike)
                {
                    _spinnerRenderer.sprite = SpinnerTypeDataBase.Data.AllTypesData[_spinnerInstanceData.Type].sprites[(int)SpinnerState.Spin];
                }
                else if(state == SpinnerState.Brake && SpinnerLocalData.Torque == 0)
                {
                    _spinnerRenderer.sprite = SpinnerTypeDataBase.Data.AllTypesData[_spinnerInstanceData.Type].sprites[4];
                }
                else
                {
                    _spinnerRenderer.sprite = SpinnerTypeDataBase.Data.AllTypesData[_spinnerInstanceData.Type].sprites[(int)state];
                }

                if(state == SpinnerState.Spin || state == SpinnerState.Strike)
                {
                    _coreRenderer.gameObject.SetActive(true);
                }
                else
                {
                    _coreRenderer.gameObject.SetActive(false);
                }
            }
        ).AddTo(this);
    }
}
