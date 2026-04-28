using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Smash")]
public class SmashEffect : PowerUpEffect
{
    [SerializeField] private  float upwardTime;
    [SerializeField] private  float force;
    [SerializeField] private  float explosionForce;
    [SerializeField] private float explosionRadius;
    
    public override void Activate(GameObject player)
    { 
        player.GetComponent<SmashBehavior>().SetReady(upwardTime, force, explosionForce, explosionRadius);
        player.GetComponent<PowerUpIndicator>().Show(Indicator, new Vector3(0, -0.5f, 0), new Vector3(2.5f, 2.5f, 2.5f));
    }

    public override void Deactivate(GameObject player)
    {
        player.GetComponent<SmashBehavior>().SetReady(false);
        player.GetComponent<PowerUpIndicator>().Hide();
    }
}
