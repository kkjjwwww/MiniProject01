using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UI_Record_Panel : MonoBehaviour
{
    [SerializeField] private GameObject recordPanel;
    [SerializeField] private Transform scrollContentTransform;
    [SerializeField] private UI_Records recordsPrefab;

    public void OpenRecordPopUp()
    {
        recordPanel.SetActive(true);
        UpdateRecordList();
    }
    public void CloseRecordPopUp()
    {
        recordPanel?.SetActive(false);
    }
    private void UpdateRecordList()
    {
        foreach (Transform child in scrollContentTransform)
        {
            Destroy(child.gameObject);
        }

        List<GameRecord> records = GameRecordManager.instance.LoadRecords();

        foreach (var record in records)
        {
            UI_Records element = Instantiate(recordsPrefab, scrollContentTransform);
            element.Init(record);
        }
    }
}
