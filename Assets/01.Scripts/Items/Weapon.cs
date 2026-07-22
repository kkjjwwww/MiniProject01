using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public WeaponData weaponData;

    public string weaponName;
    public float baseDamage;
    public float baseRange;
    public float baseCoolDown;
    public LayerMask enemyLayer;

    private float coolDownTimer;

    public virtual float FinalDamage
    {
        get
        {
            if (PlayerStats.instance != null)
            {
                return baseDamage * PlayerStats.instance.finalDamageMultiplier;
            }
            return baseDamage;
        }
    }
    public virtual float FinalCoolDown
    {
        get
        {
            if (PlayerStats.instance != null)
            {
                float cdr = Mathf.Clamp(PlayerStats.instance.finalCoolDownReduction, 0f, 0.95f);
                return baseCoolDown * (1f - cdr);
            }
            return baseCoolDown;
        }
    }
    public virtual float FinalRange => baseRange;

    protected virtual void Start()
    {
        if (weaponData != null)
        {
            weaponName = weaponData.itemName;
            baseDamage = weaponData.baseDamage;
            baseRange = weaponData.baseRange;
            baseCoolDown = weaponData.attackCoolDown;
        }
    }
    protected virtual void Update()
    {
        if (coolDownTimer > 0f)
        {
            coolDownTimer -= Time.deltaTime;
        }
    }
    public void Attack(Vector2 dir)
    {
        if (coolDownTimer > 0f) return;
        coolDownTimer = FinalCoolDown;

        CustomizeWeapon(dir);
    }
    public abstract void CustomizeWeapon(Vector2 direction);
 
}
