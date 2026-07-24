using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class UI_GameOver : MonoBehaviour
{
    public static UI_GameOver instance;

    [SerializeField] private TMP_Text timeRecordText;
    [SerializeField] private TMP_Text killRecordText;
    [SerializeField] private TMP_Text achieveCurrencyText;

    [SerializeField] private Transform itemGrid;
    [SerializeField] private UI_ItemSlot itemSlotPrefab;

    [SerializeField] private Button titleMenuButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        gameObject.SetActive(false);
    }

    public void GameOverPopUp(float timeRecord, int killRecord, List<Sprite> itemIcons, int achieveCurrency)
    {
        gameObject.SetActive(true);

        int minutes = Mathf.FloorToInt(timeRecord / 60);
        int seconds = Mathf.FloorToInt(timeRecord % 60);
        timeRecordText.text = $"생존시간 : {minutes:00}:{seconds:00}";
        killRecordText.text = $"처치한 적 : {killRecord}";
        achieveCurrencyText.text = $"획득한 군량 : +{achieveCurrency}";

        if (itemIcons != null)
        { 
        SetItemIcons(itemIcons);
        }
        Time.timeScale = 0f;
    }
    public void SetItemIcons(List<Sprite> itemIcons)
    {
        foreach(Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }
        if (itemIcons == null || itemSlotPrefab == null) return;
        foreach(Sprite icon in itemIcons)
        {
            UI_ItemSlot newSlot = Instantiate(itemSlotPrefab, itemGrid);
            newSlot.SetSlot(icon);
        }
    }
    public void TitleButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
