using UnityEngine;

/// <summary>
/// 攻撃を受けるためのインターフェース
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// 陣営取得用メソッド
    /// </summary>
    /// <returns>自分の所属する陣営</returns>
    SpinnerType GetCamp();

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <param name="attackerPosition">攻撃者の座標</param>
    /// <param name="pushPower">攻撃で押し飛ばされる距離</param>
    void RPC_ReceiveDamage(float damage, Vector2 attackerPosition, float pushPower);
}