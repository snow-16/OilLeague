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
    /// <summary> 最大サーバー接続可能プレイヤー数 </summary>
    [SerializeField]
    private int _maxConnectablePlayerCount;
    /// <summary> フリック感度 </summary>
    [SerializeField]
    private float _flickSensitivity;

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
    /// <summary> 最大サーバー接続可能プレイヤー数 </summary>
    public int MaxConnectablePlayerCount { get => _maxConnectablePlayerCount; }
    /// <summary> フリック感度 </summary>
    public float FlickSensitivity { get => _flickSensitivity; }

    /// <summary> データのインスタンス </summary>
    public static GeneralDataBase Data { get; private set; }

    public void SetData()
    {
        Data = this;
    }
}
