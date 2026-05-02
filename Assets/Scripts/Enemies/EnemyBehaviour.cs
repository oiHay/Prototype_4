using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    
    protected float MoveSpeed;
    protected Rigidbody EnemyRb;
    protected Transform Player;

    protected virtual void Awake()
    {
        EnemyRb = GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        HandleChasePlayer();
        HandleEnemyDeath();
    }

    protected virtual void HandleChasePlayer()
    {
        if(!Player) return;
        
        Vector3 lookDirection = (Player.transform.position - transform.position).normalized;
        EnemyRb.AddForce(lookDirection * MoveSpeed);
    }

    public void HandleOutOfBounder()
    {
        MoveSpeed = 0;
    }

    private void HandleEnemyDeath()
    {
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }

    public void Init(EnemyData enemyData, WaveData waveData)
    {
        data = enemyData;
        MoveSpeed = data.Speed + ((data.SpeedPerWave * waveData.CurrentWave)/2);
    }
}
