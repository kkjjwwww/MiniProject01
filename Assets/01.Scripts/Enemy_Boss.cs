using UnityEngine;

public class Enemy_Boss : Enemy
{
    [SerializeField] protected float phase2Hp = 0.5f;
    protected bool isPhase2 = false;

    [SerializeField] protected float patternInterval = 10f;
    private float patternTimer = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        isPhase2 = false;
        patternTimer = 0f;
    }
    protected override void FixedUpdate()
    {
        if (isDead) return;

        if (!isPhase2 && (currentHp / finalMaxHp) <= phase2Hp)
        {
            EnterPhase2();
        }
        patternTimer += Time.fixedDeltaTime;
        if (patternTimer >= patternInterval)
        {
            patternTimer = 0f;
            BossPattern();
        }
        base.FixedUpdate();
    }
    protected virtual void EnterPhase2()
    {
        isPhase2 = true;
        patternInterval *= 0.7f;
        Debug.Log($"Entered Phase2 boss {enemyName}");
    }
    protected virtual void BossPattern()
    {
        if (isPhase2)//2페이즈 패턴
        {

        }
        else//1페이즈 패턴
        {

        }
    }
}
