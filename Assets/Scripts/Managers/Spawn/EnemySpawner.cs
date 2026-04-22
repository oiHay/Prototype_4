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
            EnemyData data = waveData.CurrentWave >= 2
                ? enemyTypes[Random.Range(0, enemyTypes.Length)]
                : enemyTypes[0];
            
            GameObject enemy = Instantiate(data.Prefab, GenerateSpawnPoint(), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>().Init(data);
        }
    }
    
    private Vector3 GenerateSpawnPoint() // Custom método para um vector3
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange),0,Random.Range(-spawnRange,spawnRange));
    }
}
