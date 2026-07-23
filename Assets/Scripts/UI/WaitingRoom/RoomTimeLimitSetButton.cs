using UniRx;
using UnityEngine;

/// <summary>
/// 時間制限を設定するボタン
/// </summary>
public class RoomTimeLimitSetButton : BasicButton
{
    /// <summary> 制限時間を増減させる方向。0ならリセット </summary>
    [SerializeField]
    private int _timeLimitChangeDirection;

    protected override void Awake()
    {
        base.Awake();

        //ホストしか押せないように
        _canPush = NetworkingLocalData.PlayerNumber == 1;
        this.ObserveEveryValueChanged(_ => NetworkingLocalData.PlayerNumber).Subscribe(number => _canPush = number == 1);
    }

    protected override void ClickAction()
    {
        RoomServerData.Instance.RPC_UpdateTimeLimit(_timeLimitChangeDirection);
    }
}
