using System.Linq;
using UniRx;
using UnityEngine;

public class RoomSelectSpinnerButton : BasicButton, IWriteSpinnerLocal
{
    [SerializeField]
    private SpinnerType _selectableType;

    protected override void Awake()
    {
        base.Awake();

        this.ObserveEveryValueChanged(_ => SceneProcessor.State).Where(state => state == SceneState.Exist).First().Subscribe(_ =>
            {
                Observable.EveryUpdate().Select(_ => RoomServerData.Instance.Players).TakeUntil(Observable.EveryUpdate().Where(_ => SceneProcessor.State != SceneState.Exist)).Subscribe(players =>
                    {
                        _canPush = !players.Select(player => player.type).Contains(_selectableType);
                    }
                );
            }
        );
    }

    protected override void ClickAction()
    {
        RoomServerData.Instance.RPC_UpdateType(NetworkingLocalData.PlayerNumber, _selectableType);
        SpinnerDataWriter.Access(this).SetType(_selectableType);
    }
}
