using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float baseMaxHp = 100f;
    public float baseMoveSpeed = 3f;
    public float baseDamageMultiplier = 1f;
    public float baseCoolDownReduction = 0f;
    public float baseAttackSpeed = 1f;

    private float bonusMaxHp;
    private float bonusMoveSpeed;
    private float bonusDamageMultiplier;
    private float bonusCoolDownReduction;
    private float bonusAttackSpeed;

    public float finalMaxHp => baseMaxHp + bonusMaxHp;
    public float finalMoveSpeed => baseMoveSpeed + bonusMoveSpeed;
    public float finalDamageMultiplier => baseDamageMultiplier + bonusDamageMultiplier;
    public float finalCoolDownReduction => baseCoolDownReduction + bonusCoolDownReduction;
    public float finalAttackSpeed => baseAttackSpeed + bonusAttackSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    public void ModifyStat(ModifyStatType type, float value)
    {
        switch (type)
        {
            case ModifyStatType.MaxHp: bonusMaxHp += value; break;
            case ModifyStatType.MoveSpeed: bonusMoveSpeed += value; break;
            case ModifyStatType.CoolDownReduction: bonusCoolDownReduction += value;break;
            case ModifyStatType.Damage: bonusDamageMultiplier += value; break;
            case ModifyStatType.AttackSpeed: bonusAttackSpeed += value; break;
        }
    }
}
