using UnityEngine;
using UnityEngine.UI;

public class MapMarkerUIDrawer : MonoBehaviour
{
    private Transform _targetTransform;
    private float _mapRadiusRatio;

    public void Initialize(Transform targetTransform, Color markerColor, float markerSize, float mapRadiusRatio)
    {
        _targetTransform = targetTransform;
        GetComponent<Image>().color = markerColor;
        transform.localScale = Vector3.one * markerSize;
        _mapRadiusRatio = mapRadiusRatio;
        transform.localPosition = _targetTransform.position * mapRadiusRatio;
    }

    void FixedUpdate()
    {
        if(_targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.localPosition = _targetTransform.position * _mapRadiusRatio;
    }
}
