using UnityEngine;
using System.Collections.Generic;
public class UI_LevelUp : MonoBehaviour
{
    public static UI_LevelUp instance;

    [SerializeField] private GameObject UIPanel;
    [SerializeField] private List<ItemData> allItems = new List<ItemData>();

    private List<ItemData> currentSelectedCards = new List<ItemData>();

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
        currentSelectedCards.Clear();

        List<ItemData> shuffleList = new List<ItemData>(allItems);

        // 안전 장치
        int countToPick = Mathf.Min(3, shuffleList.Count);

        for (int i = 0; i < countToPick; i++)
        {
            int randomIndex = Random.Range(0, shuffleList.Count);
            currentSelectedCards.Add(shuffleList[randomIndex]);
            shuffleList.RemoveAt(randomIndex); // 중복 제거
        }

        for (int i = 0; i < currentSelectedCards.Count; i++)
        {
            ItemData item = currentSelectedCards[i];
            Debug.Log($"선택지 {i + 1}: {item.itemName} [{item.itemRarity}] - {item.itemDescription}");

            
            // 각 카드 텍스트 아이콘 갱신
        }
    }
    public void OnClickCardButton(int cardIndex)
    {
        if (cardIndex >= currentSelectedCards.Count) return;

        ItemData selectedItem = currentSelectedCards[cardIndex];

        // 인벤토리에 아이템 지급 (새로 획득하거나 레벨업 처리)
        //if (inventory.instance != null)
        //{
        //    inventory.instance.acquireitem(selecteditem);
        //}

        UIPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
