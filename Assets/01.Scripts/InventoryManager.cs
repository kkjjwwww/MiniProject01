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
        if (itemData == null) return;
        ArtifactData artifactData = itemData as ArtifactData;
        if (artifactData == null) return;

        Artifact equippedItem = equippedItems.Find(item => item.artifactData.itemID == artifactData.itemID);

        if (equippedItem != null)
        {
            equippedItem.LevelUp();
        }
        else
        {
            Artifact newItem = new Artifact(artifactData);
            if (PlayerController.instance != null)
            {
                newItem.ApplyAllEffects();
            }
            equippedItems.Add(newItem);
        }
    }

}
