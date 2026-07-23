using UnityEngine;

/// <summary>
/// システム関連の定数データベース
/// </summary>
public class GeneralDataBase : MonoBehaviour
{
    /// <summary> シーン遷移の速度 </summary>
    [SerializeField]
    private float _sceneTransitionSpeed;
    /// <summary> ゲームフィールドの半径 </summary>
    [SerializeField]
    private float _fieldRadius;
    /// <summary> 制限時間の規定値 〜秒 </summary>
    [SerializeField]
    private int _defaultTimeLimit;
    /// <summary> 制限時間の最大値 〜秒 </summary>
    [SerializeField]
    private int _maxTimeLimit;
    /// <summary> 制限時間の最少値 〜秒 </summary>
    [SerializeField]
    private int _minTimeLimit;
    /// <summary> 制限時間の設定間隔 </summary>
    [SerializeField]
    private int _timeLimitsUnit;
    /// <summary> アナウンスの最大掲示数 </summary>
    [SerializeField]
    private int _maxNoticeCount;
    /// <summary> フリック感度 </summary>
    [SerializeField]
    private float _flickSensitivity;
    /// <summary> カメラ追従速度 </summary>
    [SerializeField]
    private float _cameraFollowSpeed;
    /// <summary> カメラ追従時の最大追い越し距離 </summary>
    [SerializeField]
    private float _cameraFollowMaxOffset;
    /// <summary> ブレーキ中のカメラ帰還速度 </summary>
    [SerializeField]
    private float _cameraInBrakeResetSpeed;
    /// <summary> 最大サーバー接続可能プレイヤー数 </summary>
    [SerializeField]
    private int _maxConnectablePlayerCount;
    /// <summary> 部屋の最大人数 </summary>
    [SerializeField]
    private int _roomCapacity;
    /// <summary> ネットワークランナーのプレハブ </summary>
    [SerializeField]
    private GameObject _networkRunnerPrefab;

    /// <summary> シーン遷移の速度 </summary>
    public float SceneTransitionSpeed { get => _sceneTransitionSpeed; }
    /// <summary> ゲームフィールドの半径 </summary>
    public float FieldRadius { get => _fieldRadius; }
    /// <summary> 制限時間の規定値 〜秒 </summary>
    public int DefaultTimeLimit { get => _defaultTimeLimit; }
    /// <summary> 制限時間の最大値 〜秒 </summary>
    public int MaxTimeLimit { get => _maxTimeLimit; }
    /// <summary> 制限時間の最少値 〜秒 </summary>
    public int MinTimeLimit { get => _minTimeLimit; }
    /// <summary> 制限時間の設定間隔 </summary>
    public int TimeLimitsUnit { get => _timeLimitsUnit; }
    /// <summary> アナウンスの最大掲示数 </summary>
    public int MaxNoticeCount { get => _maxNoticeCount; }
    /// <summary> フリック感度 </summary>
    public float FlickSensitivity { get => _flickSensitivity; }
    /// <summary> カメラ追従速度 </summary>
    public float CameraFollowSpeed { get => _cameraFollowSpeed; }
    /// <summary> カメラ追従時の最大追い越し距離 </summary>
    public float CameraFollowMaxOffset { get => _cameraFollowMaxOffset; }
    /// <summary> ブレーキ中のカメラ帰還速度 </summary>
    public float CameraInBrakeResetSpeed { get => _cameraInBrakeResetSpeed; }
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
