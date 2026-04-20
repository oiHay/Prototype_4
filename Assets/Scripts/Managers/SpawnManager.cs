using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnRange = 9f;

    private void Start()
    {
        Instantiate(_enemyPrefab, GenerateSpawnPoint(), _enemyPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPoint() // Custom método para um vector3
    {
        float _spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float _spawnPosZ = Random.Range(-_spawnRange, _spawnRange);

        Vector3 _randomPos = new Vector3(_spawnPosX, 0, _spawnPosZ);
        
        return _randomPos; // essencial para retornar um valor que será usado pelo método 
    }
}
