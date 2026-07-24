using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private Transform contentBox;
    [SerializeField] private UI_ShopStatSlot itemSlotPrefab;
    [SerializeField] private TMP_Text totalCurrencyText;

    private List<UI_ShopStatSlot> spawnedSlots = new List<UI_ShopStatSlot>();

    private void OnEnable()
    {
        RefreshUI();
    }
    public void OpenShop()
    {
        gameObject.SetActive(true);
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (CurrencyManager.instance != null && totalCurrencyText != null)
        {
            totalCurrencyText.text = $"보유 군량 : {CurrencyManager.instance.currentCurrency}";
        }
        if (spawnedSlots.Count == 0 )
        {
            foreach (Transform child in contentBox)
            {
                Destroy(child.gameObject);
            }
            foreach (var statData in ShopManager.instance.allShopStats)
            {
                UI_ShopStatSlot slot = Instantiate(itemSlotPrefab, contentBox);
                slot.Init(statData, RefreshUI);
                spawnedSlots.Add(slot);
            }
        }
        else
        {
            foreach (var slot in  spawnedSlots)
            {
                slot.UpdateUI();
            }
        }
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
