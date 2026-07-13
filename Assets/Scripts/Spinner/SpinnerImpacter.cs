using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 衝撃波生成クラス
/// </summary>
public class SpinnerImpacter
{
    /// <summary>
    /// 衝撃波生成関数
    /// </summary>
    /// <param name="fireVector">衝撃波の角度</param>
    public static void FireImpact(Vector2 fireVector, bool isTurn)
    {
        List<(GameObject obj, float length)> hited = new();
        var fireAngle = Quaternion.FromToRotation(Vector2.up, fireVector).eulerAngles.z;
        var baseRayAngle = fireAngle - SpinnerParameterDataBase.Data.ImpactArc / 2;
        var waveSpawnInterval = SpinnerParameterDataBase.Data.ImpactRayCount / Mathf.FloorToInt(SpinnerParameterDataBase.Data.ImpactRayCount * SpinnerParameterDataBase.Data.ImpactWaveRatio);

        for(int i = 0; i < SpinnerParameterDataBase.Data.ImpactRayCount; i++)
        {
            var rayAngle = baseRayAngle + 360 / SpinnerParameterDataBase.Data.ImpactRayCount * i;
            var rayLength = SpinnerParameterDataBase.Data.BaseImpact;

            if(rayAngle <= baseRayAngle + SpinnerParameterDataBase.Data.ImpactArc)
            {
                rayLength += isTurn ? SpinnerParameterDataBase.Data.TurnImpactPower : SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.ImpactTorqueMultiplier;
            }

            var rayVector = Quaternion.Euler(new Vector3(0, 0, rayAngle)) * Vector2.up;
            hited.AddRange(Physics2D.RaycastAll(SpinnerLocalData.Position, rayVector, rayLength).Select(hit => (hit.collider.gameObject, rayLength)));
            Debug.DrawRay(SpinnerLocalData.Position, rayVector * rayLength, Color.red, 2);

            if((i + 1) % waveSpawnInterval == 0)
            {
                var wave = ObjectSpawner.Instance.DefaultSpawn(SpinnerParameterDataBase.Data.ImpactWavePrefab);
                wave.transform.position = SpinnerLocalData.Position;
                wave.transform.up = rayVector;
                wave.GetComponent<ImpactWaveController>().SetWave(rayLength);
            }
        }

        var attackPower = SpinnerParameterDataBase.Data.BaseAttack * SpinnerParameterDataBase.Data.AttackTorqueMultiplier * SpinnerLocalData.Torque;
        hited.GroupBy(hitData => hitData.obj).Select(hitData => hitData.First())
            .Where(hitData => hitData.obj.TryGetComponent(out IDamageable damageable) && damageable.GetCamp() != SpinnerLocalData.Type)
            .Select(hitData => (hitData.obj.GetComponent<IDamageable>(), hitData.length))
        .ToList<(IDamageable damageable, float length)>()
        .ForEach(hitData => hitData.damageable.RPC_ReceiveDamage(SpinnerLocalData.State == SpinnerState.Strike ? SpinnerLocalData.StrikeFrom : SpinnerLocalData.Type, attackPower, SpinnerLocalData.Position, hitData.length));
    }
}
