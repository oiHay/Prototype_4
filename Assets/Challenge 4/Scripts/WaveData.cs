using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Game/Wave Data")]
public class WaveData : ScriptableObject
{
    public int CurrentWave { get; private set; } = 1;

    public void NextWave() => CurrentWave++;
    public void Reset() => CurrentWave = 1;


}
