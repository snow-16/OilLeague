using UnityEngine;

public class SpinnerDamager : MonoBehaviour, IDamageable
{
    public SpinnerType GetCamp() => SpinnerLocalData.Type;

    public void ReceiveDamage(float damage, Vector2 attackerPosition, float pushPower)
    {
        
    }
}
