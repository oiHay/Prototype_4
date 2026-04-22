using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/PowerUpData")]
public class PowerUpData : ScriptableObject
{
    public PowerUpEffect Effect;
    public GameObject Prefab;
}
