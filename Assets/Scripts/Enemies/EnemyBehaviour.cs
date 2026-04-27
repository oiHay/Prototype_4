using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private WaveData _waveData;
    [SerializeField] private EnemyData _data;
    
    private float _moveSpeed;
    
    private Rigidbody _enemyRb;
    private Transform _player;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player").transform;

        _moveSpeed = _data.Speed + ((_data.SpeedPerWave * _waveData.CurrentWave)/2);
    }

    private void Update()
    {
        HandleChasePlayer();
        HandleEnemyDeath();
        
        Debug.Log("Enemy speed: " + _moveSpeed);
    }

    private void HandleChasePlayer()
    {
        if(_player == null) return;
        
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * _moveSpeed);
    }

    public void HandleOutOfBounder()
    {
        _moveSpeed = 0;
    }

    public void HandleEnemyDeath()
    {
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }

    public void Init(EnemyData data)
    {
        _data = data;
    }
}
