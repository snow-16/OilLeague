using UnityEngine;

public class SpinnerMover : MonoBehaviour, IWriteSpinnerLocal, IReceivePress, IReceiveTap, IReceiveFlick, IReceiveHold
{
    private SpinnerDataWriter _spinnerDataWriter;

    private Vector2 _velocity = Vector2.zero;

    void Awake()
    {
        var inputListDataWriter = InputListDataWriter.Access();
        inputListDataWriter.AddPressList(this);
        inputListDataWriter.AddTapList(this);
        inputListDataWriter.AddFlickList(this);
        inputListDataWriter.AddHoldList(this);

        _spinnerDataWriter = SpinnerDataWriter.Access();
    }

    void FixedUpdate()
    {
        if(SpinnerLocalData.Torque > 0)
        {
            _spinnerDataWriter.DampingTorque();
            transform.Translate(SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.SpeedTorqueMultiplier * _velocity);
            // Debug.Log(SpinnerLocalData.Torque);
        }
    }

    public void OnPress(Vector2 pressPosition)
    {
        _spinnerDataWriter.Brake();
        _velocity = _velocity.normalized * SpinnerParameterDataBase.Data.SpeedInBrake;
    }

    public void OnTap(Vector2 tapPosition)
    {
        _spinnerDataWriter.Reset();
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        _spinnerDataWriter.Turn();
        _velocity = pointerMoveVector.normalized * SpinnerParameterDataBase.Data.BaseSpeed;
    }

    public void OnHold()
    {
        _spinnerDataWriter.ChargeTorque();
    }
}
