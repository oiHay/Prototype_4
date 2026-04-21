using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    [SerializeField] private WaveData _waveData;
    
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    
    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z
    
    public int enemyCount;
    public int waveCount = 1;
    
    public GameObject player;

    private void Start()
    {
        _waveData.Reset();
        ResetPlayerPosition();
        SpawnEnemyWave(_waveData.CurrentWave);
        SpawnPowerUp();
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleEnemyCount();
    }
    
    private void HandleEnemyCount()
    {
        enemyCount = FindObjectsByType<EnemyX>().Length;
    
        if (enemyCount == 0)
        {
            _waveData.NextWave();
            ResetPlayerPosition();
            SpawnEnemyWave(_waveData.CurrentWave);
            SpawnPowerUp();
        }
    }
    
    // Generate random spawn position for powerups and enemy balls
    private Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }
    
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        //Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        { 
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    
    private void SpawnPowerUp()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }
    
    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    
    }

}
