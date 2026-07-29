using UnityEngine;
using System.Collections.Generic;
public class UI_LevelUp : MonoBehaviour
{
    public static UI_LevelUp instance;

    [SerializeField] private GameObject UIPanel;
    [SerializeField] private List<ItemData> allItems = new List<ItemData>();
    [SerializeField] private UI_LevelUpCard[] levelUpCards = new UI_LevelUpCard[3];
    [SerializeField] private Sprite healIcon;


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

        if (SFX_Manager.instance != null)
        {
            SFX_Manager.instance.PlayLevelUpSFX();
        }

        PickRandomRewards();
    }
    private void PickRandomRewards()
    {

        List<ItemData> shuffleList = allItems.FindAll(item => !MaxLevelCheck(item));

        // 안전 장치
        int countToPick = Mathf.Min(3, shuffleList.Count);

        foreach (var card in levelUpCards)
        {
            card.gameObject.SetActive(false);
        }

        if (shuffleList.Count == 0)
        {
            levelUpCards[0].SetDummyCard(healIcon);
            levelUpCards[0].gameObject.SetActive(true);
            return;
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
        else if ( selectedItem == null && PlayerStats.instance != null)
        {
            PlayerStats.instance.Heal(PlayerStats.instance.finalMaxHp * 0.3f);
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
            if (effects != null) 
            {
                if (equippedItem.currentLevel >= effects.GetMaxLevel())
                    return true;
            }
        }
        return false;
    }
}
