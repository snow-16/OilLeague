using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// カメラの後追いを操作するクラス
/// </summary>
public class SpinnerCameraController : MonoBehaviour, IReceiveFlick, IReceiveHold
{
    /// <summary> 中心からの位置補正 </summary>
    [SerializeField]
    private Vector3 _baseCameraOffset;

    /// <summary> シネマシーンカメラのインスタンス </summary>
    private CinemachineFollow _cinemachineCamera;
    /// <summary> カメラの先行距離 </summary>
    private float _lookingDistance = 0;

    void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineFollow>();
        InputListDataWriter.Access(this).AddFlickList(this).AddHoldList(this);
    }

    void FixedUpdate()
    {
        //移動中は前方方向にカメラを移動させる
        //ブレーキを開始すると徐々に中心に戻る
        if(_lookingDistance > 0)
        {
            if(SpinnerLocalData.State != SpinnerState.Brake)
            {
                _lookingDistance += GeneralDataBase.Data.CameraFollowSpeed;

                if(_lookingDistance >= GeneralDataBase.Data.CameraFollowMaxOffset)
                {
                    _lookingDistance = GeneralDataBase.Data.CameraFollowMaxOffset;
                }

                if(SpinnerLocalData.State == SpinnerState.Stop || SpinnerLocalData.State == SpinnerState.Stan)
                {
                    _lookingDistance = 0;
                }
            }

            _cinemachineCamera.FollowOffset = _baseCameraOffset + (Vector3)(SpinnerLocalData.Forword * _lookingDistance);
        }
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        _lookingDistance = 0.1f;
    }

    public void OnHold()
    {
        _lookingDistance = Mathf.Lerp(_lookingDistance, 0, GeneralDataBase.Data.CameraInBrakeResetSpeed);
    }
}
