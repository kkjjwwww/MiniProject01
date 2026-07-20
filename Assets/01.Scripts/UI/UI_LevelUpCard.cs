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

        
        decriptionText.text = itemData.itemDescription;
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
        int nextLevel = 1;
        bool isOwned = false;
        bool isMaxLevel = false;

        if (InventoryManager.instance != null)
        {
            Artifact existingItem = InventoryManager.instance.equippedItems.Find(item => item.artifactData.itemID == itemData.itemID);

            if (existingItem != null)
            {
                isOwned = true;
                nextLevel = existingItem.currentLevel;

                foreach (var effect in existingItem.artifactData.effects)
                {
                    if (effect is ArtifactEffect_StatModify statEffect)
                    {
                        if (nextLevel >= statEffect.valuePerLevel.Length)
                        {
                            isMaxLevel = true;
                        }
                        break;
                    }
                }
            }

        }
        if (isOwned)
        {
            if (isMaxLevel)
            {
                nameText.text = $"{itemData.itemName} +MAX";
            }
            else
            {
                nameText.text = $"{itemData.itemName} +{nextLevel}";
            }
        }
        else
        {
            nameText.text = itemData.itemName;
        }
    }
    
    private void SetDescriptionWithStat(ItemData itemData)
    {

    }
}
