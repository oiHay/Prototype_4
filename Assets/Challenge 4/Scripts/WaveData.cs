using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Game/Wave Data")]
public class WaveData : ScriptableObject
{
    public int CurrentWave { get; private set; } = 1;
    public float initialSpeed = 500.0f;
    public float speedPerWave = 50.0f;

    public float GetCurrentSpeed() => initialSpeed + (speedPerWave * CurrentWave);

    public void NextWave() => CurrentWave++;
    public void Reset() => CurrentWave = 1;


}
