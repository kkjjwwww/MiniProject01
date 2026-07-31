using UnityEngine;
using DG.Tweening;
public class Enemy_Boss_01 : Enemy_Boss
{
    //돌진 패턴 스탯
    public float dashDistance = 8f;
    public float dashDuration = 2f;

    [SerializeField] private SpriteRenderer warningIndicator;
    public float warningTime = 0.5f;

    private bool isDashing = false;
    private Sequence currentDashSequence;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (warningIndicator != null)
        {
            warningIndicator.gameObject.SetActive(false);

        }
    }
    protected override void BossPattern()
    {
        if (isPhase2)
        {
            ExcuteDashSequence(2);
        }
        else
        {
            ExcuteDashSequence(1);
        }
    }

    private void ExcuteDashSequence(int dashCount)
    {
        if (playerTransform == null || isDashing) return;
        
        isDashing = true;
        
        if (currentDashSequence != null && currentDashSequence.IsActive())
        {
            currentDashSequence.Kill();
        }

        currentDashSequence = DOTween.Sequence();

        for (int i = 0; i < dashCount; i++)
        {
            currentDashSequence.AppendCallback(() =>
            {
                if (playerTransform == null) return;

                Vector3 targetDir = (playerTransform.position - transform.position).normalized;
                Vector3 targetPos = transform.position + (targetDir * dashDistance);

                ShowWarningLine(targetDir);
                SFX_Manager.instance.PlaySFX(SFXType.HuaXiong_Attack);

                if (animator != null) animator.SetBool("isAttack", true);

                DOVirtual.DelayedCall(warningTime, () =>
                {
                    if (warningIndicator != null) warningIndicator.gameObject.SetActive(false);

                    transform.DOMove(targetPos, dashDuration).SetEase(Ease.Linear);
                });

            });

            currentDashSequence.AppendInterval(warningTime + dashDuration + 0.3f);
        }
        currentDashSequence.OnComplete(() =>
        {
            isDashing = false;
            if (animator != null) animator.SetBool("isAttack", false);
        });
    }
    private void ShowWarningLine(Vector3 targetDir)
    {
        if (warningIndicator == null) return;

        FlipToDirection(targetDir);

        warningIndicator.gameObject.SetActive(true);
        warningIndicator.transform.localPosition = Vector3.zero;

        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        warningIndicator.transform.rotation = Quaternion.Euler(0, 0, angle);

        SpriteRenderer indicator = warningIndicator.GetComponent<SpriteRenderer>();
        indicator.flipY = targetDir.x < 0;
    }

    protected override void MoveToPlayer()
    {
        if (isDashing) return;
        base.MoveToPlayer();
    }

    private void OnDisable()
    {
        if (currentDashSequence != null && currentDashSequence.IsActive())
        {
            currentDashSequence.Kill();
        }
        if (warningIndicator != null)
        {
            warningIndicator.DOKill();
            warningIndicator.gameObject.SetActive(false);
        }
        isDashing =false;

        if (animator != null) animator.SetBool("isAttack", false);
    }
 
}
