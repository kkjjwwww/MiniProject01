using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("체력 증가 분기")]
    public float difficultyInterval = 30f;
    [Tooltip("페이즈당 체력 증가량")]
    public float hpIncrease = 0.15f;
    [Tooltip("페이즈당 이동속도 증가량")]
    public float moveSpeedIncrease = 0.05f;

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

    [SerializeField] private List<Enemy> bossPrefabs;
    [SerializeField] private float bossInterval = 180f;
    private float bossTimer = 0f;

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
    private float currentMoveSpeedMultiplier
    {
        get
        {
            int intervalsPassed = Mathf.FloorToInt(totalGameTime / difficultyInterval);
            return 1f + (intervalsPassed * moveSpeedIncrease);
        }
    }

    //성능최적화용 변수
    public float teleportDistance = 20f;
    private float teleportTimer = 0f;
    private float teleportCoolDown = 1f;
    public float maxAliveTime = 120f;

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

        bossTimer += Time.deltaTime;
        if (bossTimer >= bossInterval)
        {
            bossTimer = 0f;
            TrySpawnBoss();
        }

        //최적화용 적 재배치
        teleportTimer += Time.deltaTime;
        if(teleportTimer >= teleportCoolDown)
        {
            teleportTimer = 0f;
            RepositionFarEnemies();
        }

        if (currentSpawnData == null) return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnData.spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnEnemy();
        }
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            DebugSpawnBoss();
        }
#endif
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
            enemy.InitEnemy(selectedPrefab, currentHpMultiplier, currentMoveSpeedMultiplier);

            activeEnemies.Add(enemy);
        }
    }
    private void TrySpawnBoss()
    {
        if (bossPrefabs == null || bossPrefabs.Count == 0)
        {
            Debug.LogWarning("SpawnManager: bossPrefabs == null");
            return;
        }
        int randomIndex = Random.Range(0,bossPrefabs.Count);
        Enemy selectedBossPrefab = bossPrefabs[randomIndex];

        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y, 0f);

        Enemy boss = ObjectPoolManager.instance.Get(selectedBossPrefab, spawnPosition, Quaternion.identity);

        if (boss != null)
        {
            boss.InitEnemy(selectedBossPrefab, currentHpMultiplier, currentMoveSpeedMultiplier,true);

            activeEnemies.Add(boss);

            if (UIManager.instance != null)
            {
                UIManager.instance.RegisterBoss(boss);
            }
        }
        
    }
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public void DebugSpawnBoss()
    {
        Debug.Log("보스 강제스폰");
        TrySpawnBoss();
    }
#endif

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

    private void RepositionFarEnemies()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = activeEnemies[i];
            if (enemy == null || !enemy.gameObject.activeSelf) continue;

            float distance = Vector3.Distance(enemy.transform.position, playerTransform.position);

            if (distance >= teleportDistance)
            {
                if (!enemy.isBoss && (Time.time - enemy.spawnTime >= maxAliveTime))
                {
                    enemy.Despawn();
                    continue;
                }
                Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
                Vector3 newSpawnPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y, 0f);

                enemy.transform.position = newSpawnPosition;
            }
        }
    }
}
