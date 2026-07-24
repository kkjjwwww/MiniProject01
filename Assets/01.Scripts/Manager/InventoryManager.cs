using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Artifact> equippedItems = new List<Artifact>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    
    public void AddItem(ItemData itemData)
    {
        //Debug.Log("add item 실행 성공");
        if (itemData == null) return;
        ArtifactData artifactData = itemData as ArtifactData;
        if (artifactData == null) return;

        Artifact equippedItem = equippedItems.Find(item => item.artifactData.itemID == artifactData.itemID);

        if (equippedItem != null)
        {
            Debug.Log("레벨업 진입시도");
            equippedItem.LevelUp();
            Debug.Log("LevelUp() 성공");
        }
        else
        {
            Artifact newItem = new Artifact(artifactData);
            if (PlayerController.instance != null)
            {
                newItem.Init(PlayerController.instance,artifactData);
            }
            equippedItems.Add(newItem);
            Debug.Log($"add new item {artifactData.itemName}");
        }
    }

}
