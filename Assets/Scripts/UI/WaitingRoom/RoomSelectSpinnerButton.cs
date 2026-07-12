using UnityEngine;

public class RoomSelectSpinnerButton : BasicButton, IWriteSpinnerLocal
{
    [SerializeField]
    private SpinnerType _selectableType;

    protected override void ClickAction()
    {
        RoomServerData.Instance.RPC_UpdateType(NetworkingLocalData.PlayerNumber, _selectableType);
        SpinnerDataWriter.Access(this).SetType(_selectableType);
    }
}
