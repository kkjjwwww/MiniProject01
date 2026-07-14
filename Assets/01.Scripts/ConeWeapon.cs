using UnityEngine;

public class ConeWeapon : Weapon
{
    //public float attackAngle = 60f;
    private ConeWeaponData coneData => weaponData as ConeWeaponData;

    public override void CustomizeWeapon(Vector2 direction)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, FinalRange, enemyLayer);

        foreach (Collider2D target in targets)
        {
            Vector2 targetDir = (target.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(direction, targetDir);

            if (angle <= coneData.attackAngle)
            {
                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.TakeDamage(FinalDamage);
                    OnHitTarget(enemy);
                }
            }

        }
    }
    protected virtual void OnHitTarget(Enemy enemy)
    {

    }
}
