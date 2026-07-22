using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;


    protected float damage;
    protected Vector3 startPosition;
    protected Projectile originPrefab;

    public virtual void Init(Vector3 startPos, Vector2 dir, float dmg, Projectile prefab)
    {
        transform.position = startPos;
        startPosition = startPos;
        damage = dmg;
        originPrefab = prefab;

        SetRotation(dir);
    }

    protected virtual void SetRotation(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void Update()
    {
        Move();
    }
    protected virtual void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                OnHitEnemy(enemy);
            }
        }
    }
    protected virtual void OnHitEnemy(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }

    public virtual void ReturnToPool()
    {
        if (ObjectPoolManager.instance != null && originPrefab != null)
        {
            ObjectPoolManager.instance.ReturnObject(originPrefab, this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
