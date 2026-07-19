using UnityEngine;

/// <summary>
/// マップ本体UI
/// </summary>
public class MapUIDrawer : MonoBehaviour
{
    /// <summary> スピナー用のマーカープレハブ </summary>
    [SerializeField]
    private GameObject _spinnerMarkerPrefab;
    /// <summary> オイルメイト用のマーカープレハブ </summary>
    [SerializeField]
    private GameObject _oilmateMarkerPrefab;

    /// <summary> マップとフィールドの大きさ比 </summary>
    private float _mapRadiusRatio;

    /// <summary> マップUIのインスタンス </summary>
    public static MapUIDrawer Instatnce { get; private set; }

    void Awake()
    {
        Instatnce = this;
        _mapRadiusRatio = transform.GetChild(0).localPosition.y / GeneralDataBase.Data.FieldRadius;
    }

    /// <summary>
    /// マーカー生成
    /// </summary>
    /// <param name="prefab">生成するマーカーのプレハブ</param>
    /// <param name="targetTransform">紐づけるオブジェクトのトランスフォーム</param>
    /// <param name="markerColor">マーカの色</param>
    /// <param name="markerSize">マーカーの大きさ</param>
    private void CreateMarker(GameObject prefab, Transform targetTransform, Color markerColor, float markerSize)
    {
        var marker = ObjectSpawner.Instance.DefaultSpawn(prefab).GetComponent<MapMarkerUIDrawer>();
        marker.transform.SetParent(transform);
        marker.Initialize(targetTransform, markerColor, markerSize, _mapRadiusRatio);
    }

    /// <summary>
    /// スピナーのマーカー生成
    /// </summary>
    /// <param name="targetTransform">紐づけるスピナーのトランスフォーム</param>
    /// <param name="type">陣営</param>
    public static void CreateSpinnerMarker(Transform targetTransform, SpinnerType type)
    {
        Instatnce.CreateMarker(Instatnce._spinnerMarkerPrefab, targetTransform, SpinnerTypeDataBase.Data.AllTypesData[type].color, type == SpinnerLocalData.Type ? 1f : 0.6f);
    }

    /// <summary>
    /// オイルメイトのマーカー生成
    /// </summary>
    /// <param name="targetTransform">紐づけるオイルメイトのトランスフォーム</param>
    /// <param name="parent">陣営</param>
    public static void CreateOilmateMarker(Transform targetTransform, SpinnerType parent)
    {
        Instatnce.CreateMarker(Instatnce._oilmateMarkerPrefab, targetTransform, SpinnerTypeDataBase.Data.AllTypesData[parent].color, 0.6f);
    }
}
