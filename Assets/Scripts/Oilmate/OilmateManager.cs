using UniRx;
using UnityEngine;

/// <summary>
/// オイルメイト生成クラス
/// </summary>
public class OilmateManager : MonoBehaviour
{
    /// <summary> 基本オイルメイトのプレハブ </summary>
    [SerializeField]
    private GameObject _oilmatePrefab;

    void Awake()
    {
        //スピナーが発進・旋回したときオイルメイトを生成する
        this.ObserveEveryValueChanged(_ => SpinnerLocalData.State).Where(state => state == SpinnerState.Spin).Subscribe(state =>
            {
                var controller = ObjectSpawner.Instance.SpawnNetwork(_oilmatePrefab, (runner, oilmate) => 
                    {
                        oilmate.transform.position = SpinnerLocalData.Position;
                        oilmate.GetComponent<OilmateController>().SetSettings(OilmateType.Drop, SpinnerLocalData.Type);
                    }
                );
            }
        );
    }
}
