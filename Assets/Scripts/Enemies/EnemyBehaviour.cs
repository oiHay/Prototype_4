using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody _enemyRb;
    private Transform _player;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        HandleChasePlayer();
    }

    private void HandleChasePlayer()
    {
        Vector3 _lookDirection = (_player.transform.position - transform.position).normalized;
        _enemyRb.AddForce(_lookDirection * _moveSpeed);
    }
}
