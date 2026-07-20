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

        nameText.text = itemData.itemName;
        decriptionText.text = itemData.itemDescription;
        iconImage.sprite = itemData.itemIcon;
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
}
