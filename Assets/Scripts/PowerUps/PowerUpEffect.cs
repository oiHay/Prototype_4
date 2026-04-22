using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    public string Name;
    public float Duration;
    public GameObject Indicator;

    public abstract void Activate(GameObject player); // abstract força que todo filho de PowerUpEffect tenha o método Active
    public abstract void Deactivate(GameObject player);
}
