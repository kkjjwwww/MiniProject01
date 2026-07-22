using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int killCount { get; private set; } = 0;  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    public void AddkillCount()
    {
        killCount++;
    }
    public void GameOver()
    {
        float timeRecord = SpawnManager.instance.totalGameTime;

        List<Sprite> itemIcons = GetItemIcons();
        UI_GameOver.instance.GameOverPopUp(timeRecord, killCount, itemIcons);
    }
    private List<Sprite> GetItemIcons()
    {
        List<Sprite> icons = new List<Sprite>();

        if (InventoryManager.instance == null || InventoryManager.instance.equippedItems == null) return icons;
        foreach (var items in InventoryManager.instance.equippedItems)
        {
            if (items.artifactData != null)
            {
                icons.Add(items.artifactData.itemIcon);
            }
        }
        return icons;
    }
}
