using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float EnemyName;
    public float Speed;
    public float Force;
    public GameObject Prefab;
}
