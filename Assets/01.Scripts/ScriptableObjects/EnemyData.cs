using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float maxHp;
    public float moveSpeed;
    public float attackDamage = 10f;
    public float expValue = 10f;
    
}
