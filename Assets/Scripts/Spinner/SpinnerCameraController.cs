using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// カメラの後追いを操作するクラス
/// </summary>
public class SpinnerCameraController : MonoBehaviour, IReceiveFlick
{
    private CinemachineFollow _cinemachineCamera;

    void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineFollow>();
        InputListDataWriter.Access(this).AddFlickList(this);
    }

    void FixedUpdate()
    {
        if(_cinemachineCamera.TrackerSettings.PositionDamping.x > 0)
        {
            _cinemachineCamera.TrackerSettings.PositionDamping *= 1 / GeneralDataBase.Data.CameraFollowSpeed;

            if(_cinemachineCamera.TrackerSettings.PositionDamping.x < 0.01f)
            {
                _cinemachineCamera.TrackerSettings.PositionDamping = Vector3.zero;
            }
        }
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        var initialVelocity = GeneralDataBase.Data.CameraFollowInitialVelocity;
        _cinemachineCamera.TrackerSettings.PositionDamping = new Vector3(initialVelocity, initialVelocity, initialVelocity);
    }
}
