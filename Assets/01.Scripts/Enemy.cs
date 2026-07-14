using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    public string enemyName;
    public float baseMaxHp;
    public float baseMoveSpeed;

    public bool isDead = false;
    public float currentHp {  get; private set; }

    private float timeHpMultiplier = 1f;
    public virtual float finalMaxHp => baseMaxHp * timeHpMultiplier;
    public virtual float finalMoveSpeed => baseMoveSpeed;

    private Enemy originPrefab;

    protected virtual void OnEnable()
    {
        if (enemyData != null)
        {
            enemyName = enemyData.name;
            baseMaxHp = enemyData.maxHp;
            baseMoveSpeed = enemyData.moveSpeed;
        }
        isDead = false;
        currentHp = finalMaxHp;
    }

    public void InitEnemy(Enemy prefab, float hpMultiplier)
    {
        SetPrefab(prefab);
        timeHpMultiplier = hpMultiplier;
        currentHp = finalMaxHp;
    }

    public virtual void TakeDamage(float damageAmount) 
    {
        if (isDead) return;

        currentHp -= damageAmount;
        Debug.Log($"{enemyName}has take damaged{damageAmount}");

        if (currentHp <= 0f)
        {
            Die();
        }
    } 
    public void SetDifficultyMultiplier(float hpMultilplier)
    {
        timeHpMultiplier = hpMultilplier;
    }

    public void SetPrefab(Enemy prefab)
    {
        originPrefab = prefab;
    }
    protected virtual void Die()
    {
        if (originPrefab != null)
        {
            ObjectPoolManager.instance.returnObject(originPrefab, this);
        }
        isDead = true;

    }
}
