using UnityEngine;

public enum ShopStatType
{
    MaxHp,
    AttackDamage,
    MoveSpeed,
    CoolDownReduction,
    CurrencyBonus,
    ExpBonus,
    MaxHp_INT,
    AttackDamage_INT,
}

[CreateAssetMenu(fileName ="NewShopStat", menuName ="Shop/Stat Data")]
public class ShopStatData : ScriptableObject
{
    public string statID;
    public string statName;
    [TextArea] public string statDescription;
    public Sprite icon;
    public ShopStatType statType;

    public int baseCost = 100;
    public int maxLevel = 5;
    public float increaseValuePerLevel;
}
