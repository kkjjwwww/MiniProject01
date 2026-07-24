using UnityEngine;

public enum ItemRarity { Normal, Rare, Unique, Legendary}
public enum ItemType { Weapon, Artifact, Partner}
public abstract class ItemData : ScriptableObject
{

    public string itemID;
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcon;
    public ItemType type;
    public ItemRarity itemRarity;
  
}
