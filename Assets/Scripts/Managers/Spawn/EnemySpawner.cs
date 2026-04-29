using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyData[] enemyTypes;
    [SerializeField] private WaveData waveData;
    [SerializeField] private float spawnRange = 9f;

    public void SpawnWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            EnemyData data = null;

            data = enemyTypes[2];

            // data = waveData.CurrentWave >= 2 
            //     ? enemyTypes[Random.Range(0, enemyTypes.Length - 1)] // Forma alternativa de fazer if-else 
            //     : enemyTypes[0];

            // if (waveData.CurrentWave == 5)
            // {
            //     ClearEnemies();
            //
            //     data = enemyTypes[2];
            // }
            
            GameObject enemy = Instantiate(data.Prefab, GenerateSpawnPoint(), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>().Init(data);
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
