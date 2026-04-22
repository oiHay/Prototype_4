using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnManager : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private WaveData _waveData;

    [SerializeField] private EnemyData[] _enemyTypes;
    [SerializeField] private GameObject _powerUpPrefab;
    
    [Header("Valores de Spawn")]
    [SerializeField] private float _spawnRange = 9f;
    [SerializeField] private int _enemyCount;

    private void OnEnable()
    {
        PlayerController.PlayerOutOfBounder += HandleReset;
    }

    private void OnDisable()
    {
        PlayerController.PlayerOutOfBounder -= HandleReset;
    }

    private void HandleReset()
    {
        _waveData.Reset();
        SpawnEnemyWave(_waveData.CurrentWave);
        SpawnPowerUp();
    }

    private void Start()
    {
        HandleReset();
    }

    private void Update()
    {
        HandleEnemyCount();
    }

    private void HandleEnemyCount()
    {
        _enemyCount = FindObjectsByType<EnemyBehaviour>().Length;
        if (_enemyCount == 0)
        {
            _waveData.NextWave();
            SpawnEnemyWave(_waveData.CurrentWave);
            SpawnPowerUp();
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            EnemyData data = _waveData.CurrentWave >= 2
                ? _enemyTypes[Random.Range(0, _enemyTypes.Length)]
                : _enemyTypes[0];
            
            GameObject enemy = Instantiate(data.Prefab, GenerateSpawnPoint(), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>().Init(data);
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
