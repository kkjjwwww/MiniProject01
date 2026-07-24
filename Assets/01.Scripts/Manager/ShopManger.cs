using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopManger : MonoBehaviour
{
    public static ShopManger instance;

    public List<ShopStatData> allShopStats;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public int GetStatLevel(ShopStatData data)
    {
        return PlayerPrefs.GetInt($"ShopLevel_{data.statID}", 0);
    }
    public int GetUpgradeCost(ShopStatData data)
    {
        int currentLevel = GetStatLevel(data);
        return Mathf.RoundToInt(data.baseCost * Mathf.Pow(2, currentLevel));
    }

    public bool TryUpgradeStat(ShopStatData data)
    {
        int currentLevel = GetStatLevel(data);

        if (currentLevel == data.maxLevel)
        {
            Debug.Log("«ÿ¥Á Ω∫≈» ∏∏∑æ");
            return false;
        }

        int cost = GetUpgradeCost(data);

        if (CurrencyManager.instance == null || !CurrencyManager.instance.UseCurrency(cost))
        {
            Debug.Log("¿Á»≠ ∫Œ¡∑");
            return false;
        }

        PlayerPrefs.SetInt($"ShopLevel_{data.statID}", currentLevel + 1);
        PlayerPrefs.Save();
        return true;
    }

    public float GetTotalBonusValue(ShopStatType type)
    {
        float totalBonus = 0f;
        foreach (var stat in allShopStats)
        {
            if (stat.statType == type)
            {
                int level = GetStatLevel(stat);
                totalBonus += level * stat.increaseValuePerLevel;
            }
        }
        return totalBonus;
    }
}
