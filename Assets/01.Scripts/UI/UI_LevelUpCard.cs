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
            if (effect == null) continue;

            string statText = effect.GetDescriptionText(targetIndex);

            if (!string.IsNullOrEmpty(statText))
            {
                finalDescription += $"\n\n<color=#00FF00>{statText}</color>";
            }
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
            if (effect == null)
                continue;

            if (item.currentLevel + 1 >= effect.GetMaxLevel())
                return true;
        }
        return false;
    }
    
}
