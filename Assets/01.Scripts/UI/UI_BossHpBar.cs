using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_BossHpBar : MonoBehaviour
{
    [SerializeField] private TMP_Text bossNameText;
    [SerializeField] private Slider bossHpSlider;

    public Enemy targetBoss { get; private set; }

    public void Init(Enemy boss)
    {
        targetBoss = boss;
        if (bossNameText != null) bossNameText.text = boss.enemyName;
        UpdateHp();
    }

    public void UpdateHp()
    {
        if (targetBoss == null || bossHpSlider == null) return;

        bossHpSlider.maxValue = targetBoss.finalMaxHp;
        bossHpSlider.value = Mathf.Clamp(targetBoss.currentHp, 0f, targetBoss.finalMaxHp);
    }
}
