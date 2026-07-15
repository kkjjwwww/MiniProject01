using UnityEngine;

[CreateAssetMenu(fileName = "NewStatModifyEffect", menuName = "Item/Effects/StatModify")]
public class ArtifactEffect_StatModify : ArtifactEffect
{
    public ModifyStatType statType;
    public float modifyValue;

    public override void OnApply(PlayerController player, int level)
    {
        float totalValue = modifyValue * level;
        if (PlayerStats.instance != null)
        {
            PlayerStats.instance.ModifyStat(statType, totalValue);
        }
    }

    public override void OnRemove(PlayerController player, int level)
    {
        float totalValue = modifyValue * level;
        if (PlayerStats.instance != null)
        {
            PlayerStats.instance.ModifyStat(statType, -totalValue);
        }
    }
}
