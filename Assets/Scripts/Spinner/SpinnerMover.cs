using UnityEngine;

/// <summary>
/// スピナーの移動処理クラス
/// </summary>
public class SpinnerMover : MonoBehaviour, IWriteSpinnerLocal
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;
    /// <summary> スピナーインスタンスデータ閲覧用 </summary>
    private SpinnerInstanceData _spinnerInstanceData;

    void Awake()
    {
        _spinnerDataWriter = SpinnerDataWriter.Access(this);
        _spinnerInstanceData = GetComponent<SpinnerInstanceData>();
    }

    void FixedUpdate()
    {
        if(_spinnerInstanceData.Type == SpinnerLocalData.Type)
        {
            if(SpinnerLocalData.Torque > 0)
            {
                float spinSpeed;
                switch(SpinnerLocalData.State)
                {
                    case SpinnerState.Brake: 
                    {
                        spinSpeed = SpinnerParameterDataBase.Data.SpeedInBrake;
                        break;
                    }
                    case SpinnerState.Strike: 
                    {
                        spinSpeed = SpinnerParameterDataBase.Data.StrikeSpeed;
                        break;
                    }
                    default :
                    {
                        spinSpeed = SpinnerParameterDataBase.Data.BaseSpeed * SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.SpeedTorqueMultiplier;
                        break;  
                    }
                };

                transform.Translate(spinSpeed * SpinnerLocalData.Forword, Space.World);
                _spinnerDataWriter.SavePosition(transform);

                if(transform.position.magnitude > GeneralDataBase.Data.FieldRadius - transform.localScale.x / 2)
                {
                    var reflectVector = Vector2.Reflect(SpinnerLocalData.Forword, transform.position.normalized);
                    transform.position = transform.position.normalized * (GeneralDataBase.Data.FieldRadius - transform.localScale.x / 2);
                    _spinnerDataWriter.UpdateForword(reflectVector);
                    _spinnerDataWriter.SavePosition(transform);
                    SpinnerImpacter.FireImpact(-transform.position.normalized, true);
                }

                _spinnerDataWriter.DampingTorque();

                if(SpinnerLocalData.State == SpinnerState.Strike && SpinnerLocalData.Torque <= SpinnerParameterDataBase.Data.MaxTorque)
                {
                    _spinnerDataWriter.Data.SetState(SpinnerState.Spin);
                }
            }
        }
    }
}
