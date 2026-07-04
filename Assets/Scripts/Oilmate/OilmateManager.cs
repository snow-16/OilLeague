using UnityEngine;

public class OilmateManager : MonoBehaviour, IReceiveFlick
{
    [SerializeField]
    private GameObject _oilmatePrefab;

    void Awake()
    {
        InputListDataWriter.Access().AddFlickList(this);
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        Instantiate(_oilmatePrefab, SpinnerLocalData.Position, Quaternion.identity);
    }
}
