using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies =11;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new  UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps;
    private bool isSpawing = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestoyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void EnemyDestoyed()
    {
        enemiesAlive--;
        LevelManager.main.killpoint();
    }

    private void Update()
    {
        if (!isSpawing) return;
        timeSinceLastSpawn += Time.deltaTime; 
        if(timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if(enemiesAlive ==0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }
    private IEnumerator  StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawing=true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }   
    
    private void EndWave()
    {
        isSpawing = false;
        currentWave++;
        timeSinceLastSpawn= 0f;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    { 
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index]; 
        Instantiate(prefabToSpawn,LevelManager.main.StartPoint.position,Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return  Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}
