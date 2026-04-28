using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Gun")]
public class GunEffect : PowerUpEffect
{
    [SerializeField] private  float speed = 50f;
    [SerializeField] private float fireInterval = 0.5f;
    [SerializeField] private  GameObject projectile;
    
    public override void Activate(GameObject player)
    { 
        player.GetComponent<ProjectileShooter>().StartShooting(speed, fireInterval, projectile);
        player.GetComponent<PowerUpIndicator>().Show(Indicator, new Vector3(0, -0.5f, 0), new Vector3(2.5f, 2.5f, 2.5f));
    }

    public override void Deactivate(GameObject player)
    {
        player.GetComponent<ProjectileShooter>().StopShooting();
        player.GetComponent<PowerUpIndicator>().Hide();
    }
}
