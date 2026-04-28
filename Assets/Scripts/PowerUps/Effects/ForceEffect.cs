using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Force")]
public class ForceEffect : PowerUpEffect
{
    [SerializeField] private float strength = 30f;

    public override void Activate(GameObject player)
    { 
        player.GetComponent<PlayerCollision>().SetForce(true, strength);
        player.GetComponent<PowerUpIndicator>().Show(Indicator, new Vector3(0, -0.5f, 0), new Vector3(2.5f, 2.5f, 2.5f));
    }

    public override void Deactivate(GameObject player)
    {
        player.GetComponent<PlayerCollision>().SetForce(false);
        player.GetComponent<PowerUpIndicator>().Hide();
    }
}
