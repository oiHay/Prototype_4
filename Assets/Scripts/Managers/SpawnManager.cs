using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnManager : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _powerUpPrefab;
    
    [Header("Valores de Spawn")]
    [SerializeField] private float _spawnRange = 9f;

    [Header("Valores de Wave e Inimigo")]
    [SerializeField] private int _enemyCount;
    [SerializeField] private int _waveNumber = 1;

    private void Start()
    {
        SpawnEnemyWave(_waveNumber);
        SpawnPowerUp();
    }

    private void Update()
    {
        _enemyCount = FindObjectsByType<EnemyBehaviour>().Length;
        if (_enemyCount == 0)
        {
            _waveNumber++;
            SpawnEnemyWave(_waveNumber);
            SpawnPowerUp();
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(_enemyPrefab, GenerateSpawnPoint(), _enemyPrefab.transform.rotation);
        }
    }
    
    private void SpawnPowerUp()
    {
        Instantiate(_powerUpPrefab, GenerateSpawnPoint(), _powerUpPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPoint() // Custom método para um vector3
    {
        float _spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float _spawnPosZ = Random.Range(-_spawnRange, _spawnRange);

        Vector3 _randomPos = new Vector3(_spawnPosX, 0, _spawnPosZ);
        
        return _randomPos; // essencial para retornar um valor que será usado pelo método 
    }
}
