using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    public string name;
    public float baseMaxHp;
    public float baseMoveSpeed;

    public bool isDead = false;

    public float currentHp {  get; private set; }

    public virtual float finalMaxHp => baseMaxHp;
    public virtual float finalMoveSpeed => baseMoveSpeed;

    private Enemy originPrefab;

    protected virtual void Start()
    {
        if (enemyData != null)
        {
            name = enemyData.name;
            baseMaxHp = enemyData.maxHp;
            baseMoveSpeed = enemyData.moveSpeed;
        }

        currentHp = finalMaxHp;
    }

    protected virtual void TakeDamage(float damageAmount) 
    {
        if (isDead) return;

        currentHp -= damageAmount;
        Debug.Log($"{name}has take damaged{damageAmount}");

        if (currentHp <= 0f)
        {
            Die();
        }
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
