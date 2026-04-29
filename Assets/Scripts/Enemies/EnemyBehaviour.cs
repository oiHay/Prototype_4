using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private WaveData waveData;
    [SerializeField] private EnemyData data;
    
    private float moveSpeed;
    
    private Rigidbody enemyRb;
    private Transform player;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;

        moveSpeed = data.Speed + ((data.SpeedPerWave * waveData.CurrentWave)/2);
    }

    private void Update()
    {
        HandleChasePlayer();
        HandleEnemyDeath();
        
        Debug.Log("Enemy speed: " + moveSpeed);
    }

    private void HandleChasePlayer()
    {
        if(player == null) return;
        
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * moveSpeed);
    }

    public void HandleOutOfBounder()
    {
        moveSpeed = 0;
    }

    private void HandleEnemyDeath()
    {
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }

    public void Init(EnemyData enemyData)
    {
        data = enemyData;
    }
}
