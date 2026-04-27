using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Gun")]
public class GunEffect : PowerUpEffect
{
    public float Speed = 50f;
    public float FireInterval = 0.5f;
    public GameObject Projectile;
    
    public override void Activate(GameObject player)
    { 
        player.GetComponent<PlayerCollision>().EnableGun(Speed, FireInterval, Projectile, Indicator);
    }

    public override void Deactivate(GameObject player)
    {
        player.GetComponent<PlayerCollision>().DisableGun();
    }
}
