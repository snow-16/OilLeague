using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マップ上のマーカーUI
/// </summary>
public class MapMarkerUIDrawer : MonoBehaviour
{
    /// <summary> 紐付けされたオブジェクトのトランスフォーム </summary>
    private Transform _targetTransform;
    /// <summary> マップとフィールドの大きさ比 </summary>
    private float _mapRadiusRatio;

    /// <summary>
    /// マーカー初期設定
    /// </summary>
    /// <param name="targetTransform">紐づけるオブジェクトのトランスフォーム</param>
    /// <param name="markerColor">マーカーの色</param>
    /// <param name="markerSize">マーカーの大きさ</param>
    /// <param name="mapRadiusRatio">マップとフィールドの大きさ比</param>
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
