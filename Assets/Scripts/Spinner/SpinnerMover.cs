using UnityEngine;

public class SpinnerMover : MonoBehaviour, IWriteSpinnerLocal, IReceivePress, IReceiveTap, IReceiveFlick, IReceiveHold
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;

    /// <summary> ブレーキ中の経過時間 </summary>
    private float _progressBrakeTime = 0;

    void Awake()
    {
        var inputListDataWriter = InputListDataWriter.Access();
        inputListDataWriter.AddPressList(this);
        inputListDataWriter.AddTapList(this);
        inputListDataWriter.AddFlickList(this);
        inputListDataWriter.AddHoldList(this);

        _spinnerDataWriter = SpinnerDataWriter.Access();
        _spinnerDataWriter.SetType(SpinnerType.Red);
        _spinnerDataWriter.SavePosition(transform);
    }

    void FixedUpdate()
    {
        if(SpinnerLocalData.Torque > 0)
        {
            var baseSpeed = SpinnerParameterDataBase.Data.BaseSpeed * SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.SpeedTorqueMultiplier;
            transform.Translate((SpinnerLocalData.State.Equals(SpinnerState.Brake) ? SpinnerParameterDataBase.Data.SpeedInBrake : baseSpeed) * SpinnerLocalData.Forword, Space.World);
            _spinnerDataWriter.SavePosition(transform);
            _spinnerDataWriter.DampingTorque();
        }
    }

    public void OnPress(Vector2 pressPosition)
    {
        _spinnerDataWriter.Brake();
    }

    public void OnTap(Vector2 tapPosition)
    {
        if(SpinnerLocalData.Torque > 0)
        {
            SpinnerImpacter.FireImpact(transform.up, false);
            _spinnerDataWriter.Stop();
        }
    }

    public void OnFlick(Vector2 pointerMoveVector)
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

    public void OnHold()
    {
        _spinnerDataWriter.ChargeTorque();

        if(_progressBrakeTime < SpinnerParameterDataBase.Data.QuickTurnTimeLimit)
        {
            _progressBrakeTime += Time.deltaTime;
        }
    }
}
