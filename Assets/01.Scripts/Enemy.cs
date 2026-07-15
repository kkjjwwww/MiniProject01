using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

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
    private Rigidbody2D rb;
    protected Transform playerTransform;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Tweener hitTweener;
    
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    protected virtual void OnEnable()
    {
        spriteRenderer.color = originalColor;
        if (enemyData != null)
        {
            enemyName = enemyData.name;
            baseMaxHp = enemyData.maxHp;
            baseMoveSpeed = enemyData.moveSpeed;
        }
        isDead = false;
        if (PlayerController.instance != null)
        {
            playerTransform = PlayerController.instance.transform;
        }
        if (rb == null ) rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

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
        Debug.Log($"{enemyName}has take damaged{damageAmount} {currentHp}/{finalMaxHp}");
        if (spriteRenderer != null )
        {
            if (hitTweener != null && hitTweener.IsActive())
            {
                hitTweener.Kill();
                spriteRenderer.color = originalColor;
            }
            spriteRenderer.color = Color.red;
            hitTweener = spriteRenderer.DOColor(originalColor, 0.2f).SetEase(Ease.OutQuad);
        }

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
        if (SpawnManager.instance != null)
        {
            SpawnManager.instance.OnEnemyDespawn(this);
        }
        if (originPrefab != null)
        {
            ObjectPoolManager.instance.returnObject(originPrefab, this);
        }
        isDead = true;

    }
    private void OnDisable()
    {
        KillTween();
    }
    protected virtual void FixedUpdate()
    {
        if (isDead || playerTransform == null) 
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveToPlayer();
    }

    protected virtual void MoveToPlayer()
    {
        Vector3 dir = (playerTransform.position - transform.position).normalized;
            
            rb.linearVelocity = dir * finalMoveSpeed;
    }
    private void KillTween()
    {
        if (hitTweener.IsActive())
        {
            hitTweener.Kill();
        }
        spriteRenderer.color = originalColor;
    }
    private void OnDestroy()
    {
        KillTween();
    }
}
