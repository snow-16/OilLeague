using UnityEngine;

public class MapUIDrawer : MonoBehaviour
{
    [SerializeField]
    private GameObject _spinnerMarkerPrefab;
    [SerializeField]
    private GameObject _oilmateMarkerPrefab;

    private float _mapRadiusRatio;

    public static MapUIDrawer Instatnce { get; private set; }

    void Awake()
    {
        Instatnce = this;
        _mapRadiusRatio = transform.GetChild(0).localPosition.y / GeneralDataBase.Data.FieldRadius;
        Debug.Log(_mapRadiusRatio);
    }

    private void CreateMarker(GameObject prefab, Transform targetTransform, Color markerColor, float markerSize)
    {
        var marker = ObjectSpawner.Instance.DefaultSpawn(prefab).GetComponent<MapMarkerUIDrawer>();
        marker.transform.SetParent(transform);
        marker.Initialize(targetTransform, markerColor, markerSize, _mapRadiusRatio);
    }

    public static void CreateSpinnerMarker(Transform targetTransform, SpinnerType type)
    {
        Instatnce.CreateMarker(Instatnce._spinnerMarkerPrefab, targetTransform, SpinnerTypeDataBase.Data.AllTypesData[type].color, type == SpinnerLocalData.Type ? 1f : 0.6f);
    }

    public static void CreateOilmateMarker(Transform targetTransform, SpinnerType parent)
    {
        Instatnce.CreateMarker(Instatnce._oilmateMarkerPrefab, targetTransform, SpinnerTypeDataBase.Data.AllTypesData[parent].color, 0.6f);
    }
}
