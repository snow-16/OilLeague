using UnityEngine;

public class SpinnerImpacter : MonoBehaviour, IReceiveFlick, IReceiveTap
{
    void Awake()
    {
        var inputListDataWriter = InputListDataWriter.Access();
        inputListDataWriter.AddFlickList(this);
        inputListDataWriter.AddTapList(this);
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        FireImpact(pointerMoveVector.normalized);
    }

    public void OnTap(Vector2 tapPosition)
    {
        FireImpact(-transform.up);
    }

    private void FireImpact(Vector2 fireVector)
    {
        
    }
}
