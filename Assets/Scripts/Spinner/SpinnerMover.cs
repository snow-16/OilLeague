using UnityEngine;

/// <summary>
/// スピナーの移動処理クラス
/// </summary>
public class SpinnerMover : MonoBehaviour, IWriteSpinnerLocal
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;

    void Awake()
    {
        _spinnerDataWriter = SpinnerDataWriter.Access();
    }

    void FixedUpdate()
    {
        if(SpinnerLocalData.Torque > 0)
        {
            var baseSpeed = SpinnerParameterDataBase.Data.BaseSpeed * SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.SpeedTorqueMultiplier;
            transform.Translate((SpinnerLocalData.State.Equals(SpinnerState.Brake) ? SpinnerParameterDataBase.Data.SpeedInBrake : baseSpeed) * SpinnerLocalData.Forword, Space.World);
            _spinnerDataWriter.SavePosition(transform);
            _spinnerDataWriter.DampingTorque();

            if(SpinnerLocalData.State == SpinnerState.Stan && SpinnerLocalData.Torque <= SpinnerParameterDataBase.Data.MaxTorque)
            {
                _spinnerDataWriter.Data.SetState(SpinnerState.Spin);
            }
        }
    }
}
