using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    private const string CURRENCY_KEY = "UserCurrency";
    public int currentCurrency {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCurrency();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadCurrency()
    {
        currentCurrency= PlayerPrefs.GetInt(CURRENCY_KEY, 0);
    }

    public void AddCurrency(int value)
    {
        if (value <= 0) return;
        currentCurrency += value;
        SaveCurrency();
    }
    public void SaveCurrency()
    {
        PlayerPrefs.SetInt(CURRENCY_KEY, currentCurrency);
        PlayerPrefs.Save();
    }

    public bool UseCurrency(int value)
    {
        if (value >= currentCurrency)
        {
            currentCurrency -= value;
            return true; 
        }
        return false;
    }
}
