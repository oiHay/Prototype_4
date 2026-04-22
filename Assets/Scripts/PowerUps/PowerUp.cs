using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpData _data;
    public PowerUpData Data => _data;
}