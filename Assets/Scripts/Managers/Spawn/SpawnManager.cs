using System;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] private WaveData waveData;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PowerUpSpawner powerUpSpawner;

    private void OnEnable() => PlayerController.PlayerOutOfBounder += HandleReset;

    private void OnDisable() => PlayerController.PlayerOutOfBounder -= HandleReset;

    private void Start() => HandleReset();

    private void Update()
    {
        if (FindObjectsByType<EnemyBehaviour>().Length == 0)
        {
            waveData.NextWave();
            enemySpawner.SpawnWave(waveData.CurrentWave);
            powerUpSpawner.SpawnPowerUp();
        }
    }

    private void HandleReset()
    {
        waveData.Reset();
        enemySpawner.ClearEnemies();
        powerUpSpawner.ClearPowerUps();
        enemySpawner.SpawnWave(waveData.CurrentWave);
        powerUpSpawner.SpawnPowerUp();
    }
}
