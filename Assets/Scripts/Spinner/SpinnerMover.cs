using UnityEngine;

public class SpinnerMover : MonoBehaviour, IReceiveTap, IReceiveFlick, IReceiveHold
{
    private Vector2 _velocity = Vector2.zero;

    void Awake()
    {
        var dataWriter = InputListDataWriter.Access();
        dataWriter.AddTapList(this);
        dataWriter.AddFlickList(this);
        dataWriter.AddHoldList(this);
    }

    void FixedUpdate()
    {
        transform.Translate(_velocity);
    }

    public void OnTap(Vector2 tapPosition)
    {
        Debug.Log("タップ");
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        Debug.Log("フリック");
        _velocity = pointerMoveVector.normalized * 0.5f;
    }

    public void OnHold()
    {
        Debug.Log("ホールド");
    }
}
