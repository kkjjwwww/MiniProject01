using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    public string name;
    public float baseMaxHp;
    public float baseMoveSpeed;

    public bool isDead = false;

    public float currentHp {  get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
