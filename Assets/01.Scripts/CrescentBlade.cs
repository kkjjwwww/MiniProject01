using UnityEngine;

public class CrescentBlade : ConeWeapon
{
    public float knockbackValue = 5f;

    protected override void OnHitTarget(Enemy enemy)
    {
        base.OnHitTarget(enemy);
        if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D enemyRb))
        {
            Vector2 dir = (enemy.transform.position - transform.position).normalized;
            enemyRb.AddForce(dir * knockbackValue, ForceMode2D.Impulse);
        }
    }
}
