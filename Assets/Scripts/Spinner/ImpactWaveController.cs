using UnityEngine;

/// <summary>
/// 衝撃波演出の制御クラス
/// </summary>
public class ImpactWaveController : MonoBehaviour
{
    /// <summary> 衝撃波の移動距離 </summary>
    private float _waveLength;
    /// <summary> 衝撃波の中心位置 </summary>
    private Vector2 _centorPosition;

    void FixedUpdate()
    {
        transform.Translate(0, _waveLength / SpinnerParameterDataBase.Data.ImpactWaveSpeed, 0);
        if(((Vector2)transform.position - _centorPosition).magnitude > _waveLength)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 衝撃波の初期設定
    /// </summary>
    /// <param name="length">移動距離</param>
    public void SetWave(float length)
    {
        _waveLength = length - transform.localScale.x;
        _centorPosition = transform.position;
    }
}
