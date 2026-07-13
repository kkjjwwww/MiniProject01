using UnityEngine;


public enum ItemType { Weapon, Artifact, Consumable }
public abstract class ItemData : ScriptableObject
{

    public string itemID;
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcon;
    public ItemType type;
    
}
