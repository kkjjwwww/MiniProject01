using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("체력 증가 분기")]
    public float difficultyInterval = 30f;
    [Tooltip("페이즈당 체력 증가량")]
    public float hpIncrease = 0.15f;

    public static SpawnManager instance;

    [System.Serializable]
    public class Phase
    {
        public string phaseName;
        public float startPhaseTimeSec;
        public SpawnData spawnData;
    }
    public List<Phase> timelinePhases;
    public float totalGameTime {  get; private set; }

    public float spawnRadius = 12f;

    int currentPhaseIndex = -1;
    private List<Enemy> activeEnemies = new List<Enemy>();
    private SpawnData currentSpawnData;
    private float spawnTimer;

    private Transform playerTransform;

    private float currentHpMultiplier
    {
        get
        {
            int intervalsPassed = Mathf.FloorToInt(totalGameTime / difficultyInterval);
            return 1f + (intervalsPassed * hpIncrease);
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {  
        if (transform.parent != null)
        {
            playerTransform = transform.parent;
        }
        CheckPhase();
    }
    private void Update()
    {
        if ( playerTransform == null) return;

        totalGameTime += Time.deltaTime;

        CheckPhase();

        if (currentSpawnData == null) return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnData.spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnEnemy();
        }
    }
    private void TrySpawnEnemy()
    {
        if (activeEnemies.Count >= currentSpawnData.maxCountEnemy) return;

        Enemy selectedPrefab = currentSpawnData.GetRandomEnemyPrefab();
        if (selectedPrefab == null) return;

        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y, 0f);

        Enemy enemy = ObjectPoolManager.instance.Get(selectedPrefab, spawnPosition, Quaternion.identity);

        if (enemy != null)
        {
            enemy.InitEnemy(selectedPrefab, currentHpMultiplier);

            activeEnemies.Add(enemy);
        }
    }
    public void OnEnemyDespawn(Enemy enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }
    private void CheckPhase()
    {
        int targetPhaseIndex = -1;
        for (int i = timelinePhases.Count - 1; i >= 0; i--)
        {
            if (totalGameTime >= timelinePhases[i].startPhaseTimeSec)
            {
                targetPhaseIndex = i;
                break;
            }
        }
        if (targetPhaseIndex != -1 && targetPhaseIndex != currentPhaseIndex)
        {
            currentPhaseIndex = targetPhaseIndex;
            currentSpawnData = timelinePhases[currentPhaseIndex].spawnData;
            spawnTimer = 0f;
            Debug.Log($"현재 페이즈: {timelinePhases[currentPhaseIndex].phaseName}");
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 centerPosition = transform.position;
        if (playerTransform != null)
        {
            centerPosition = playerTransform.position;
        }
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(centerPosition, spawnRadius);
    }
}
