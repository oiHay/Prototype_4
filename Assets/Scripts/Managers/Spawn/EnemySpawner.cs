using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyData[] enemyTypes;
    [SerializeField] private EnemyData bossData;
    [SerializeField] private WaveData waveData;
    [SerializeField] private float spawnRange = 9f;

    private int bossCount = 0;

    public void SpawnWave(int enemiesToSpawn)
    {
        if (waveData.CurrentWave % 5 == 0) 
        {
            ClearEnemies();
            bossCount++;
            
            for (int i = 0; i < bossCount; i++)
            {
                GameObject boss = Instantiate(bossData.Prefab, GenerateSpawnPoint(), Quaternion.identity);
                boss.GetComponent<EnemyBehaviour>().Init(bossData, waveData);
            }
            return;
        }
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            
            EnemyData data = waveData.CurrentWave >= 2 
                ? enemyTypes[Random.Range(0, enemyTypes.Length)] // Forma alternativa de fazer if-else 
                : enemyTypes[0];
            
            GameObject enemy = Instantiate(data.Prefab, GenerateSpawnPoint(), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>().Init(data, waveData);
        }
    }
    
    private Vector3 GenerateSpawnPoint() // Custom método para um vector3
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange),0,Random.Range(-spawnRange,spawnRange));
    }
    
    public void ClearEnemies()
    {
        foreach (var enemies in FindObjectsByType<EnemyBehaviour>() )
        {
            Destroy(enemies.gameObject);
        }
    }
}
