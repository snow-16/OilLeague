using UnityEngine;

public class SpinnerMover : MonoBehaviour, IReceiveTap, IReceiveFlick, IReceiveHold
{
    void Awake()
    {
        var dataWriter = InputListDataWriter.Access();
        dataWriter.AddTapList(this);
        dataWriter.AddFlickList(this);
        dataWriter.AddHoldList(this);
    }

    void FixedUpdate()
    {
        
    }

    public void OnTap(Vector2 tapPosition)
    {
        Debug.Log("タップ");
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        Debug.Log("フリック");
    }

    public void OnHold()
    {
        Debug.Log("ホールド");
    }
}
