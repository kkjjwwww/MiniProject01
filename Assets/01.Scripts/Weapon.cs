using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string name;
    public float baseDamage;
    public float baseRange;
    public LayerMask enemyLayer;

    public abstract void UserWeapon(Vector2 direction);
 
}
