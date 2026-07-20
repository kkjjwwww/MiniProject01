using UnityEngine;

[CreateAssetMenu(fileName = "NewStatModifyEffect", menuName = "Item/Effects/StatModify")]
public class ArtifactEffect_StatModify : ArtifactEffect
{
    public ModifyStatType statType;
    public float[] valuePerLevel;

    public override void OnApply(PlayerController player, int level)
    {

        if (PlayerStats.instance == null) return;
        int index = level - 1;
        if (index >= 0 && index < valuePerLevel.Length)
        {
            float applyValue = valuePerLevel[index];
        
            PlayerStats.instance.ModifyStat(statType, applyValue);
        }
    }

    public override void OnRemove(PlayerController player, int level)
    {

        if (PlayerStats.instance == null) return;
        
        int index = level - 1; 

        if (index >= 0 && index < valuePerLevel.Length)
        {
            float applyValue = valuePerLevel[index];
            PlayerStats.instance.ModifyStat(statType, -applyValue);
        }
    }
}
