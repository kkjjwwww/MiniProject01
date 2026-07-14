using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public WeaponData weaponData;

    public string weaponName;
    public float baseDamage;
    public float baseRange;
    public LayerMask enemyLayer;

    public virtual float FinalDamage => baseDamage;
    public virtual float FinalRange => baseRange;

    protected virtual void Start()
    {
        if (weaponData != null)
        {
            weaponName = weaponData.itemName;
            baseDamage = weaponData.baseDamage;
            baseRange = weaponData.baseRange;

        }
    }
    public abstract void UserWeapon(Vector2 direction);
 
}
