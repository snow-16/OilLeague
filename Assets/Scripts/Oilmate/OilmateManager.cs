using UnityEngine;

/// <summary>
/// オイルメイト生成クラス
/// </summary>
public class OilmateManager : MonoBehaviour, IReceiveFlick
{
    /// <summary> 基本オイルメイトのプレハブ </summary>
    [SerializeField]
    private GameObject _oilmatePrefab;

    void Awake()
    {
        InputListDataWriter.Access().AddFlickList(this);
    }

    public void OnFlick(Vector2 pointerMoveVector)
    {
        if(SpinnerLocalData.State != SpinnerState.Stan)
        {
            var controller = Instantiate(_oilmatePrefab, SpinnerLocalData.Position, Quaternion.identity).GetComponent<OilmateController>();
            controller.SetSettings(OilmateType.Drop, SpinnerType.Red);
        }
    }
}
