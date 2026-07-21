using UnityEngine;
using System.Collections.Generic;
public class UI_LevelUp : MonoBehaviour
{
    public static UI_LevelUp instance;

    [SerializeField] private GameObject UIPanel;
    [SerializeField] private List<ItemData> allItems = new List<ItemData>();
    [SerializeField] private UI_LevelUpCard[] levelUpCards = new UI_LevelUpCard[3];

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OpenLevelUpWindow()
    {
        if (UIPanel == null) return;

        Time.timeScale = 0f;
        UIPanel.SetActive(true);

        PickRandomRewards();
    }
    private void PickRandomRewards()
    {

        List<ItemData> shuffleList = new List<ItemData>(allItems);

        // 안전 장치
        int countToPick = Mathf.Min(3, shuffleList.Count);

        foreach (var card in levelUpCards)
        {
            card.gameObject.SetActive(false);
        }

        for (int i = 0; i < countToPick; i++)
        {
            int randomIndex = Random.Range(0, shuffleList.Count);
            ItemData pickedItem = shuffleList[randomIndex];
            shuffleList.RemoveAt(randomIndex); // 중복 제거

            levelUpCards[i].TextSet(pickedItem);
            levelUpCards[i].gameObject.SetActive(true);
        }
    }
    public void OnClickCardButton(int cardIndex)
    {
        if (cardIndex >= levelUpCards.Length || !levelUpCards[cardIndex].gameObject.activeSelf) return;

        ItemData selectedItem = levelUpCards[cardIndex].assingedItem;

        if (InventoryManager.instance != null && selectedItem !=null)
        {
            InventoryManager.instance.AddItem(selectedItem);
        }

        
        Time.timeScale = 1f;
        UIPanel.SetActive(false);
    }
    private bool MaxLevelCheck(ItemData itemData)
    {
        Artifact equippedItem = InventoryManager.instance.equippedItems.Find(item => item != null && item.artifactData.itemID == itemData.itemID);

        if (equippedItem == null) return false;
        if (equippedItem.artifactData == null || equippedItem.artifactData.effects == null) return false;

        foreach (var effects in equippedItem.artifactData.effects)
        {
            if (effects is ArtifactEffect_StatModify statEffect)
            {
                if(statEffect != null && equippedItem.currentLevel >= statEffect.valuePerLevel.Length)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
