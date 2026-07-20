using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUpCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI decriptionText;
    [SerializeField] Image iconImage;

    public ItemData assingedItem { get; private set; }

    public void TextSet(ItemData itemData)
    {
        if (itemData == null) return;

        assingedItem = itemData;

        SetNameWithLevel(itemData);

        SetDescriptionWithStat(itemData);
        iconImage.sprite = itemData.itemIcon;

        RarityNameColor(itemData.itemRarity);
    }

    private void RarityNameColor(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Normal: nameText.color = Color.white; break;
            case ItemRarity.Rare: nameText.color = new Color(65f,105f,225f); break;
            case ItemRarity.Unique: nameText.color = new Color(255f, 215f, 0); break;
            case ItemRarity.Legendary: nameText.color = new Color(255f, 69f, 0); break;
        }
    }
    //중복아이템 드롭시 레벨업 표시
    private void SetNameWithLevel(ItemData itemData)
    {
        Artifact equippedItem = GetEquippedArtifact(itemData);
        if (equippedItem == null)
        {
            nameText.text = itemData.itemName;
            return;
        }
        bool isMaxLevel = IsMaxLevelArtifact(equippedItem);
        nameText.text = isMaxLevel ? $"{itemData.itemName} +MAX" : $"{itemData.itemName} +{equippedItem.currentLevel}";
    } 
        
    
    private void SetDescriptionWithStat(ItemData itemData)
    {
        string finalDescription = itemData.itemDescription; 
        ArtifactData artifactData = itemData as ArtifactData;

        if (artifactData == null || artifactData.effects == null) 
        {
            decriptionText.text = finalDescription; 
            return;
        }

        Artifact equippedItem = GetEquippedArtifact(itemData);
        int targetIndex = (equippedItem != null) ? equippedItem.currentLevel : 0;

        foreach (var effect in artifactData.effects)
        {
            if (effect is not ArtifactEffect_StatModify statEffect) continue;

            int index = Mathf.Min(targetIndex, statEffect.valuePerLevel.Length - 1);
            if (index < 0 || index >= statEffect.valuePerLevel.Length) continue;

            float statValue = statEffect.valuePerLevel[index];
            string statText = GetStatTextByFormat(statEffect.statType, statValue);

            finalDescription += $"\n\n<color=#00FF00>{statText}</color>";
        }
        decriptionText.text = finalDescription;
    }
    private Artifact GetEquippedArtifact(ItemData itemData)
    {
        if (InventoryManager.instance == null) return null; 
        return InventoryManager.instance.equippedItems.Find(item => item.artifactData.itemID == itemData.itemID); 
    }
    private bool IsMaxLevelArtifact(Artifact item)
    {
        if (item == null || item.artifactData == null || item.artifactData.effects == null) return false;

        foreach (var effect in item.artifactData.effects)
        {
            if (effect == null || effect is not ArtifactEffect_StatModify statEffect)
                continue; 

            if (statEffect.valuePerLevel == null)
                continue;

            return item.currentLevel >= statEffect.valuePerLevel.Length; 
        }
        return false;
    }
    private string GetStatTextByFormat(ModifyStatType type, float value)
    {
        string statName = type switch
        {
            ModifyStatType.MaxHp => "최대 체력",
            ModifyStatType.MoveSpeed => "이동 속도",
            ModifyStatType.Damage => "공격력",
            ModifyStatType.AttackSpeed => "공격 속도",
            ModifyStatType.CoolDownReduction => "쿨타임 감소",
            _ => type.ToString()
        };

        // 수치 뒤에 % 붙일 대상
        return type switch
        {
            ModifyStatType.Damage or ModifyStatType.AttackSpeed or ModifyStatType.CoolDownReduction or ModifyStatType.MoveSpeed
                => $"{statName} +{value * 100f}%",
            _ => $"{statName} +{value}"
        };
    }
}
