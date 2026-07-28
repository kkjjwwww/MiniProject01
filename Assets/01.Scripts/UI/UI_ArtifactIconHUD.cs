using UnityEngine;
using System.Collections.Generic;

public class UI_ArtifactIconHUD : MonoBehaviour
{
    public static UI_ArtifactIconHUD instance;

    [SerializeField] private Transform iconParent;
    [SerializeField] private UI_ArtifactIcon iconPrefab;

    private List<UI_ArtifactIcon> activeIcons = new List<UI_ArtifactIcon>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        RefreshArtifactUI();
    }
    public void RefreshArtifactUI()
    {
        if (InventoryManager.instance == null) return;

        var items = InventoryManager.instance.equippedItems;
        if (items == null) return;

        for (int i = 0; i < activeIcons.Count; i++)
        {
            activeIcons[i].gameObject.SetActive(false);
        }
        for (int i =0; i < items.Count; i++)
        {
            Artifact artifact = items[i];
            if (artifact == null) continue;

            UI_ArtifactIcon iconSlot;

            if (i < activeIcons.Count)
            {
                iconSlot = activeIcons[i];
            }
            else
            {
                iconSlot = Instantiate(iconPrefab, iconParent);
                activeIcons.Add(iconSlot);
            }
            iconSlot.gameObject.SetActive(true);
            iconSlot.SetArtifact(artifact);
        }
    }
}
