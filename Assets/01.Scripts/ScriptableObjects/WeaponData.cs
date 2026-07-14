using UnityEngine;

[CreateAssetMenu(fileName ="NewWeaponData",menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ItemData
{
    public float baseDamage;
    public float baseRange;
    public float attackAngle;
    public float attackCoolDown;
}
