using UnityEngine;

[CreateAssetMenu(fileName = "NewStatModifyEffect", menuName = "Item/Effects/StatModify")]
public class ArtifactEffect_StatModify : ArtifactEffect
{
    public ModifyStatType statType;
    public float[] valuePerLevel;

    public override void OnApply(PlayerController player, int level)
    {

        if (PlayerStats.instance == null) return;

        int index = Mathf.Clamp(level -1, 0 ,valuePerLevel.Length - 1);
        
        float applyValue = valuePerLevel[index];
        PlayerStats.instance.ModifyStat(statType, applyValue);
        
    }

    public override void OnRemove(PlayerController player, int level)
    {

        if (PlayerStats.instance == null) return;

        int index = Mathf.Clamp(level - 1, 0, valuePerLevel.Length - 1);

        float applyValue = valuePerLevel[index];
        PlayerStats.instance.ModifyStat(statType, -applyValue);
        
    }
    public override int GetMaxLevel()
    {
        return valuePerLevel != null ? valuePerLevel.Length  : 0;
    }
    public override string GetDescriptionText(int level)
    {
        int index = Mathf.Clamp(level, 0, valuePerLevel.Length - 1);
        float value = valuePerLevel[index];

        string statName = statType switch
        {
            ModifyStatType.MaxHp => "최대 체력",
            ModifyStatType.MoveSpeed => "이동 속도",
            ModifyStatType.Damage => "공격력",
            ModifyStatType.CoolDownReduction => "공격 속도",
            _ => statType.ToString()
        };

        return (statType == ModifyStatType.MaxHp ||
                statType == ModifyStatType.MoveSpeed ||
                statType == ModifyStatType.Damage ||
                statType == ModifyStatType.CoolDownReduction)
                ? $"{statName} +{value * 100f}%"
                : $"{statName} +{value}";
    }
}
