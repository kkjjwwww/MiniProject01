using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopStatSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button upgradeButton;

    private ShopStatData currentData;
    private System.Action upgradeCallBack;

    public void Init(ShopStatData data, System.Action upgrade)
    {
        currentData = data;
        upgradeCallBack = upgrade;

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(UpgradeClicked);

        UpdateUI();
    }
    public void UpdateUI()
    {
        if (currentData == null) return;   

        iconImage.sprite = currentData.icon;
        nameText.text = currentData.statName;
        descriptionText.text = string.Format(currentData.statDescription,currentData.increaseValuePerLevel);

        int currentLevel = ShopManager.instance.GetStatLevel(currentData);
        bool isMax = currentLevel >= currentData.maxLevel;

        levelText.text = isMax ? $"Lv. {currentLevel} (MAX)" : $"Lv. {currentLevel}";    

        if (isMax)
        {
            priceText.text = "-";
            upgradeButton.interactable = false;
        }
        else
        {
            int price = ShopManager.instance.GetUpgradeCost(currentData);
            priceText.text = $"{price}";

            int currentCurrency = CurrencyManager.instance.currentCurrency;
            upgradeButton.interactable = currentCurrency >= price;
        }
    }

    private void UpgradeClicked()
    {
        if (ShopManager.instance.TryUpgradeStat(currentData))
        {
            upgradeCallBack?.Invoke();
        }
    }
}
