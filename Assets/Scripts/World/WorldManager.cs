using UnityEngine;

/// <summary>
/// مدیریت دنیای بازی
/// </summary>
public class WorldManager : MonoBehaviour
{
    [Header("دشمنان")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialEnemyCount = 5;
    [SerializeField] private float spawnRadius = 50f;
    [SerializeField] private float minSpawnDistance = 10f;
    
    [Header("نقاط اسپاون")]
    [SerializeField] private Transform spawnPoint;
    
    private Transform playerTransform;
    private static WorldManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        
        // اسپاون دشمنان اولیه
        SpawnInitialEnemies();
    }

    /// <summary>
    /// اسپاون دشمنان اولیه
    /// </summary>
    private void SpawnInitialEnemies()
    {
        for (int i = 0; i < initialEnemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// اسپاون یک دشمن جدید
    /// </summary>
    private void SpawnEnemy()
    {
        if (enemyPrefab == null || playerTransform == null)
            return;
        
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// دریافت موقعیت تصادفی اسپاون
    /// </summary>
    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += playerTransform.position;
        randomDirection.y = playerTransform.position.y;
        
        // اطمینان از فاصله کافی از بازیکن
        while (Vector3.Distance(randomDirection, playerTransform.position) < minSpawnDistance)
        {
            randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += playerTransform.position;
            randomDirection.y = playerTransform.position.y;
        }
        
        return randomDirection;
    }

    /// <summary>
    /// اسپاون دشمن جدید هنگام مرگ دشمن دیگر
    /// </summary>
    public void RequestEnemySpawn()
    {
        Invoke("SpawnEnemy", 2f);
    }

    public static WorldManager Instance => instance;
}
