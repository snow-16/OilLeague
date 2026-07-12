using UnityEngine;

/// <summary>
/// システム関連の定数データベース
/// </summary>
public class GeneralDataBase : MonoBehaviour
{
    /// <summary> ゲームフィールドの半径 </summary>
    [SerializeField]
    private float _fieldRadius;
    /// <summary> 制限時間の規定値 〜分 </summary>
    [SerializeField]
    private float _defaultTimeLimit;
    /// <summary> 制限時間の最大値 〜分 </summary>
    [SerializeField]
    private float _maxTimeLimit;
    /// <summary> 制限時間の設定間隔 </summary>
    [SerializeField]
    private float _timeLimitsUnit;
    /// <summary> アナウンスの最大掲示数 </summary>
    [SerializeField]
    private int _maxNoticeCount;
    /// <summary> フリック感度 </summary>
    [SerializeField]
    private float _flickSensitivity;
    /// <summary> カメラ追従速度 </summary>
    [SerializeField]
    private float _cameraFollowSpeed;
    /// <summary> カメラ追従初速 </summary>
    [SerializeField]
    private float _cameraFollowInitialVelocity;
    /// <summary> 最大サーバー接続可能プレイヤー数 </summary>
    [SerializeField]
    private int _maxConnectablePlayerCount;
    /// <summary> 部屋の最大人数 </summary>
    [SerializeField]
    private int _roomCapacity;
    /// <summary> ネットワークランナーのプレハブ </summary>
    [SerializeField]
    private GameObject _networkRunnerPrefab;

    /// <summary> ゲームフィールドの半径 </summary>
    public float FieldRadius { get => _fieldRadius; }
    /// <summary> 制限時間の規定値 〜分 </summary>
    public float DefaultTimeLimit { get => _defaultTimeLimit; }
    /// <summary> 制限時間の最大値 〜分 </summary>
    public float MaxTimeLimit { get => _maxTimeLimit; }
    /// <summary> 制限時間の設定間隔 </summary>
    public float TimeLimitsUnit { get => _timeLimitsUnit; }
    /// <summary> アナウンスの最大掲示数 </summary>
    public int MaxNoticeCount { get => _maxNoticeCount; }
    /// <summary> フリック感度 </summary>
    public float FlickSensitivity { get => _flickSensitivity; }
    /// <summary> カメラ追従速度 </summary>
    public float CameraFollowSpeed { get => _cameraFollowSpeed; }
    /// <summary> カメラ追従初速 </summary>
    public float CameraFollowInitialVelocity { get => _cameraFollowInitialVelocity; }
    /// <summary> 最大サーバー接続可能プレイヤー数 </summary>
    public int MaxConnectablePlayerCount { get => _maxConnectablePlayerCount; }
    /// <summary> 部屋の最大人数 </summary>
    public int RoomCapacity { get => _roomCapacity; }
    /// <summary> ネットワークランナーのプレハブ </summary>
    public GameObject NetworkRunnerPrefab { get => _networkRunnerPrefab; }

    /// <summary> データのインスタンス </summary>
    public static GeneralDataBase Data { get; private set; }

    public void SetData()
    {
        Data = this;
    }
}
