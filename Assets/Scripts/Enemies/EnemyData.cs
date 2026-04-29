using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public float Speed;
    public float SpeedPerWave;
    public float Force;
    public GameObject Prefab;
}
