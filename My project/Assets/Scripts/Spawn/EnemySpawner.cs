using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    public int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private float timer = 0;

    void Start()
    {
        ResetSpawner();
    }

    void Update()
    {
        // Spawning enemy secara otomatis dengan interval waktu
        if (isSpawning)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval && spawnCount > 0)
            {
                SpawnEnemy();
                timer = 0;
            }
        }
    }

    // Method untuk spawn enemy
    public void SpawnEnemy()
    {
        if (spawnedEnemy == null) return;

        Enemy enemy = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
        enemy.spawner = this;
        combatManager.totalEnemies++;
        spawnCount--;
    }

    // Reset spawner untuk wave baru
    public void ResetSpawner()
    {
        spawnCount = defaultSpawnCount + (spawnCountMultiplier - 1) * multiplierIncreaseCount;
        totalKillWave = spawnCount;
    }

    // Method untuk mencatat kill dan memeriksa kondisi wave
    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave--;

        // Tingkatkan jumlah spawn jika mencapai jumlah minimum kill
        if (totalKill % minimumKillsToIncreaseSpawnCount == 0)
        {
            spawnCountMultiplier++;
        }

        // Jika semua musuh dalam wave ini mati, periksa wave berikutnya
        if (totalKillWave <= 0)
        {
            combatManager.CheckWaveCompletion();
        }
    }
}
