using UnityEngine;

public class ImpactWaveController : MonoBehaviour
{
    private float _waveLength;
    private Vector2 _spawnedPosition;

    void FixedUpdate()
    {
        transform.Translate(0, _waveLength / SpinnerParameterDataBase.Data.ImpactWaveSpeed, 0);
        if(((Vector2)transform.position - _spawnedPosition).magnitude > _waveLength)
        {
            Destroy(gameObject);
        }
    }

    public void SetWave(float length)
    {
        _waveLength = length - transform.localScale.x;
        _spawnedPosition = transform.position;
    }
}
