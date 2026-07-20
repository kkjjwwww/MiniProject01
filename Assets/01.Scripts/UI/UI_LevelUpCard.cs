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

        if (InventoryManager.instance != null)
        {
            Artifact existingItem = InventoryManager.instance.equippedItems.Find(item => item.artifactData.itemID == itemData.itemID);

            if (existingItem != null)
            {
                isOwned = true;
                nextLevel = existingItem.currentLevel + 1;
            }
        }
        if (isOwned)
        {
            nameText.text = $"{itemData.name} +{nextLevel}";
        }
        else
        {
            nameText.text = itemData.name;
        }
    }
}
