using UnityEngine;

public class ConeWeapon : Weapon
{
    //public float attackAngle = 60f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private AttackEffect attackEffectPrafab;
    private ConeWeaponData coneData => weaponData as ConeWeaponData;

    public override void CustomizeWeapon(Vector2 direction)
    {
        SpawnAttackEffect();

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
    private void SpawnAttackEffect()
    {
        if (attackEffectPrafab == null || ObjectPoolManager.instance == null) return;

        Transform pivot = PlayerController.instance.RotationPivot;

        Vector3 spawnPos = pivot != null ? pivot.position : transform.position;
        Quaternion spawnRot = Quaternion.identity;
        if(pivot != null)
        {
            spawnRot = pivot.rotation * attackEffectPrafab.transform.rotation;
        }
        AttackEffect effect = ObjectPoolManager.instance.Get(attackEffectPrafab, spawnPos, spawnRot);

        float rangeMultiplier = 1f;
        if (baseRange >0f)
        {
            rangeMultiplier = FinalRange / baseRange;
        }
        effect.Init(attackEffectPrafab, pivot, rangeMultiplier);

        
    }
    protected virtual void OnHitTarget(Enemy enemy)
    {

    }
    private void DrawRange()
    {

    }
}
