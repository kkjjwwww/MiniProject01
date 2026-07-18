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

        if (itemData.type == ItemType.Weapon)
        {
            HandleWeaponAcquisition(itemData);
        }
        else if (itemData.type == ItemType.Artifact)
        {
            HandleArtifactAcquisition(itemData as ArtifactData);
        }
    }
    private void HandleWeaponAcquisition(ItemData itemData)
    {
        Weapon existingWeapon = equippedWeapons.Find(w => w.weaponData.itemID == itemData.itemID);

        if (existingWeapon != null)
        {

            // existingWeapon.LevelUp();
            Debug.Log($"{itemData.itemName} 무기 레벨업");
        }
        else
        {
            // 새로 획득한 무기라면 생성 후 리스트에 추가
            // (예시: 프리팹 목록에서 알맞은 무기를 찾아 플레이어 자식으로 스폰)
            Weapon targetPrefab = weaponPrefabs.Find(w => w.weaponData.itemID == itemData.itemID);
            if (targetPrefab != null)
            {
                Weapon newWeapon = Instantiate(targetPrefab, transform.position, Quaternion.identity, transform);
                // newWeapon.Init();
                equippedWeapons.Add(newWeapon);
                Debug.Log($"새 무기 장착: {itemData.itemName}");
            }
        }
    }
}
