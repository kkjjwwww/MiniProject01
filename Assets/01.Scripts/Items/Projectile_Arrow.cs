using UnityEngine;

public class Projectile_Arrow : Projectile
{
    [SerializeField] private float maxRange = 10f;
    private bool isReturned = false;

    protected override void Update()
    {
        base.Update();

        if(Vector3.Distance(startPosition,transform.position) >= maxRange )
        {
            ReturnToPool();
        }
    }
    public override void Init(Vector3 startPos, Vector2 dir, float dmg, Projectile prefab)
    {
        base.Init(startPos, dir, dmg, prefab);
        isReturned = false;
    }
    protected override void OnHitEnemy(Enemy enemy)
    {
        base.OnHitEnemy(enemy);

        ReturnToPool();
    }
    public override void ReturnToPool()
    {
        if (isReturned) return;
        isReturned = true;

        if (originPrefab is Projectile_Arrow arrowPrefab)
        {
            ObjectPoolManager.instance.ReturnObject(arrowPrefab, this);
            return;
        }
        base.ReturnToPool();
    }
}
