using UnityEngine;

public class SpinnerMover : MonoBehaviour, IWriteSpinnerLocal, IReceivePress, IReceiveTap, IReceiveFlick, IReceiveHold
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;

    /// <summary> 移動ベクトル </summary>
    private Vector2 _velocity = Vector2.zero;

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
            _spinnerDataWriter.DampingTorque();
            transform.Translate(SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.SpeedTorqueMultiplier * _velocity, Space.World);
            _spinnerDataWriter.SavePosition(transform);
        }
    }

    public void OnPress(Vector2 pressPosition)
    {
        _spinnerDataWriter.Brake();
        _velocity = _velocity.normalized * SpinnerParameterDataBase.Data.SpeedInBrake;
    }

    public void OnTap(Vector2 tapPosition)
    {
        _spinnerDataWriter.Stop();
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        _spinnerDataWriter.Turn();
        _velocity = pointerMoveVector.normalized * SpinnerParameterDataBase.Data.BaseSpeed;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, pointerMoveVector);
    }

    public void OnHold()
    {
        _spinnerDataWriter.ChargeTorque();
    }
}
