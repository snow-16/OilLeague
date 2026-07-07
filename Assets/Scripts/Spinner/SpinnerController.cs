using UnityEngine;

/// <summary>
/// スピナーへの入力処理クラス
/// </summary>
public class SpinnerController : MonoBehaviour, IWriteSpinnerLocal, IReceivePress, IReceiveTap, IReceiveFlick, IReceiveHold
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;
    /// <summary> スピナーインスタンスデータ閲覧用 </summary>
    private SpinnerInstanceData _spinnerInstanceData;
    
    /// <summary> ブレーキ中の経過時間 </summary>
    private float _progressBrakeTime = 0;

    void Awake()
    {
        _spinnerInstanceData = GetComponent<SpinnerInstanceData>();

        if(SpinnerLocalData.Type == _spinnerInstanceData.Type)
        {
            var inputListDataWriter = InputListDataWriter.Access();
            inputListDataWriter.AddPressList(this);
            inputListDataWriter.AddTapList(this);
            inputListDataWriter.AddFlickList(this);
            inputListDataWriter.AddHoldList(this);

            _spinnerDataWriter = SpinnerDataWriter.Access();
            _spinnerDataWriter.SavePosition(transform);
        }
    }

    public void OnPress(Vector2 pressPosition)
    {
        if(SpinnerLocalData.State != SpinnerState.Stan)
        {
            _spinnerDataWriter.Brake();
        }
    }

    public void OnTap(Vector2 tapPosition)
    {
        if(SpinnerLocalData.State == SpinnerState.Brake)
        {
            SpinnerImpacter.FireImpact(transform.up, false);
            _spinnerDataWriter.Stop();
        }
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        if(SpinnerLocalData.State == SpinnerState.Brake)
        {
            var turnVelocity = pointerMoveVector.normalized;

            //素早くフリックしたらベクトル置き換えではなく加算にする
            if(_progressBrakeTime <= SpinnerParameterDataBase.Data.QuickTurnTimeLimit && SpinnerLocalData.Torque > 0)
            {
                turnVelocity = (turnVelocity + (Vector2)transform.up).normalized;
            }

            _progressBrakeTime = 0;

            _spinnerDataWriter.Turn();
            transform.rotation = Quaternion.FromToRotation(Vector2.up, turnVelocity);
            _spinnerDataWriter.UpdateForword(transform.up);
            SpinnerImpacter.FireImpact(-transform.up, true);
        }
    }

    public void OnHold()
    {
        if(SpinnerLocalData.State == SpinnerState.Brake)
        {
            _spinnerDataWriter.ChargeTorque();

            if(_progressBrakeTime < SpinnerParameterDataBase.Data.QuickTurnTimeLimit)
            {
                _progressBrakeTime += Time.deltaTime;
            }
        }
    }
}
