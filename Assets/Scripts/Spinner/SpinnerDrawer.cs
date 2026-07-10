using UniRx;
using UnityEngine;

/// <summary>
/// スピナーの描画関係を処理するクラス
/// </summary>
public class SpinnerDrawer : MonoBehaviour
{
    /// <summary> スピナーのスプライトレンダラ </summary>
    private SpriteRenderer _spinnerRenderer;
    /// <summary> スピナーインスタンスデータ閲覧用 </summary>
    private SpinnerInstanceData _spinnerInstanceData;

    void Awake()
    {
        _spinnerRenderer = GetComponent<SpriteRenderer>();
        _spinnerInstanceData = GetComponent<SpinnerInstanceData>();
    }

    void FixedUpdate()
    {
        //回転
        if(SpinnerLocalData.Torque > 0 && SpinnerLocalData.State != SpinnerState.Brake)
        {
            transform.eulerAngles += new Vector3(0, 0, SpinnerLocalData.Torque);
        }
        else if(transform.eulerAngles != Vector3.zero)
        {
            transform.eulerAngles = Vector3.zero;
        }

        //方向
        if(SpinnerLocalData.Forword.x < 0)
        {
            _spinnerRenderer.flipX = false;
        }
        else
        {
            _spinnerRenderer.flipX = true;
        }

        this.ObserveEveryValueChanged(_ => SpinnerLocalData.State).Subscribe(state =>
            {
                if(state == SpinnerState.Strike)
                {
                    _spinnerRenderer.sprite = SpinnerTypeDataBase.Data.AllTypesData[_spinnerInstanceData.Type].sprites[(int)SpinnerState.Spin];
                }
                else
                {
                    _spinnerRenderer.sprite = SpinnerTypeDataBase.Data.AllTypesData[_spinnerInstanceData.Type].sprites[(int)state];
                }
            }
        );
    }
}
