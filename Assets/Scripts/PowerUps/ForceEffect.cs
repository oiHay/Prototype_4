using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Force")]
public class ForceEffect : PowerUpEffect
{
    public float Strength = 30f;

    public override void Activate(GameObject player)
    { 
        player.GetComponent<PlayerCollision>().EnableForce(Strength, Indicator);
    }

    public override void Deactivate(GameObject player)
    {
        player.GetComponent<PlayerCollision>().DisableForce();
    }
}
