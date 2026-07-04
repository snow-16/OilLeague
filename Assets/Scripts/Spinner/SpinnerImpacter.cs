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
        List<GameObject> hited = new();
        var fireAngle = Quaternion.FromToRotation(Vector2.up, fireVector).eulerAngles.z;
        var baseRayAngle = fireAngle - SpinnerParameterDataBase.Data.ImpactArc / 2;

        for(int i = 0; i < SpinnerParameterDataBase.Data.ImpactRayCount; i++)
        {
            var rayAngle = baseRayAngle + 360 / SpinnerParameterDataBase.Data.ImpactRayCount * i;
            var rayLength = SpinnerParameterDataBase.Data.BaseImpact;

            if(rayAngle <= baseRayAngle + SpinnerParameterDataBase.Data.ImpactArc)
            {
                rayLength *= isTurn ? SpinnerParameterDataBase.Data.TurnImpactPower : SpinnerLocalData.Torque * SpinnerParameterDataBase.Data.ImpactTorqueMultiplier;
            }

            hited.AddRange(Physics2D.RaycastAll(SpinnerLocalData.Position, new Vector3(0, 0, rayAngle), rayLength).Select(hit => hit.collider.gameObject));
            Debug.DrawRay(SpinnerLocalData.Position, Quaternion.Euler(new Vector3(0, 0, rayAngle)) * Vector2.up * rayLength, Color.red, 2);
        }

        hited = hited.Where(obj => hited.Count(checkObj => obj == checkObj) == 1).ToList();
    }
}
