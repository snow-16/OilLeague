using Fusion;
using UniRx;
using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// スピナーへの入力処理クラス
/// </summary>
public class SpinnerController : NetworkBehaviour, IWriteSpinnerLocal, IReceivePress, IReceiveTap, IReceiveFlick, IReceiveHold
{
    /// <summary> スピナーデータアクセス用 </summary>
    private SpinnerDataWriter _spinnerDataWriter;
    /// <summary> スピナーインスタンスデータ閲覧用 </summary>
    private SpinnerInstanceData _spinnerInstanceData;
    
    /// <summary> ブレーキ中の経過時間 </summary>
    private float _progressBrakeTime = 0;

    public override void Spawned()
    {
        _spinnerInstanceData = GetComponent<SpinnerInstanceData>();

        if(SpinnerLocalData.Type == _spinnerInstanceData.Type)
        {
            var inputListDataWriter = InputListDataWriter.Access(this);
            inputListDataWriter.AddPressList(this);
            inputListDataWriter.AddTapList(this);
            inputListDataWriter.AddFlickList(this);
            inputListDataWriter.AddHoldList(this);

            _spinnerDataWriter = SpinnerDataWriter.Access(this);
            _spinnerDataWriter.SavePosition(transform);
            FindAnyObjectByType<CinemachineCamera>().Target.TrackingTarget = transform;

            _spinnerInstanceData.RPC_SetState(SpinnerLocalData.State);
            this.ObserveEveryValueChanged(_ => SpinnerLocalData.State).Subscribe(state =>
                {
                    _spinnerInstanceData.RPC_SetState(state);
                }
            ).AddTo(this);

            _spinnerInstanceData.RPC_SetForword(SpinnerLocalData.Forword);
            this.ObserveEveryValueChanged(_ => SpinnerLocalData.Forword).Subscribe(forword =>
                {
                    _spinnerInstanceData.RPC_SetForword(forword);
                }
            ).AddTo(this);
        }

        MapUIDrawer.CreateSpinnerMarker(transform, _spinnerInstanceData.Type);
    }

    public void OnPress(Vector2 pressPosition)
    {
        if(SpinnerLocalData.State != SpinnerState.Stan && SpinnerLocalData.State != SpinnerState.Strike)
        {
            _spinnerDataWriter.Brake();
        }
    }

    public void OnTap(Vector2 tapPosition)
    {
        if(SpinnerLocalData.State == SpinnerState.Brake)
        {
            SpinnerImpacter.FireImpact(SpinnerLocalData.Forword, false);
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
                turnVelocity = (turnVelocity + SpinnerLocalData.Forword).normalized;
            }

            _progressBrakeTime = 0;

            _spinnerDataWriter.Turn();
            _spinnerDataWriter.UpdateForword(turnVelocity);
            SpinnerImpacter.FireImpact(-turnVelocity, true);
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
