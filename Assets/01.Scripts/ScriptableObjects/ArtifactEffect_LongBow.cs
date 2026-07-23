using UnityEngine;


[CreateAssetMenu(fileName ="LongBowEffect",menuName = "Artifact/Effecs/LongBow")]
public class ArtifactEffect_LongBow : ArtifactEffect
{
    [SerializeField] Projectile_Arrow arrowPrefab;
    [SerializeField] float coolDown = 1.5f;

    private float[] damageRatio = { 0.5f, 0.75f, 1f, 1.2f };
    private float coolDownTimer = 0f;
    
    public override int GetMaxLevel()
    {
        return damageRatio != null ? damageRatio.Length  : 0;
    }

    public override void OnUpdate(PlayerController player, int level)
    {
        if (player == null || arrowPrefab == null) return;
        
        Weapon weapon = player.CurrentWeapon;
        if (weapon == null) return;
        Transform pivot = player.RotationPivot;
        if (pivot == null) return; 

        coolDownTimer += Time.deltaTime;

        if (coolDownTimer >= coolDown)
        {
            coolDownTimer = 0f;
            
            Vector3 spawnPos = pivot.position;
            Vector2 dir = pivot.right;

            Projectile_Arrow arrow = ObjectPoolManager.instance.Get(arrowPrefab, spawnPos,Quaternion.identity);

            int levelIndex = Mathf.Clamp(level - 1, 0, damageRatio.Length - 1);
            float finalDamage = weapon.FinalDamage * damageRatio[levelIndex];

            arrow.Init(spawnPos, dir, finalDamage, arrowPrefab);
        }
    }
    public override string GetDescriptionText(int level)
    {
        int index = Mathf.Clamp(level, 0, damageRatio.Length - 1);
        float percentage = damageRatio[index] * 100f;
        return $"공격력의 {percentage}%";
    }
}
