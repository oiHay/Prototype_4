using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private PowerUpData[] PowerUpTypes;
    [SerializeField] private float spawnRange = 9f;

    public void SpawnPowerUp()
    {
        PowerUpData data = PowerUpTypes[Random.Range(0, PowerUpTypes.Length)];
        Instantiate(data.Prefab, GenerateSpawnPoint(), Quaternion.identity);
    }
    
    private Vector3 GenerateSpawnPoint() // Custom método para um vector3
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange),0,Random.Range(-spawnRange,spawnRange));
    }
}
