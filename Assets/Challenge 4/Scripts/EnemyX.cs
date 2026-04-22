using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    [SerializeField] private WaveData _waveData;
    [SerializeField] private EnemyData _data;
    
    private float _speed;
    private Rigidbody enemyRb;
    private Transform playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.FindWithTag("PlayerGoal").transform;

        _speed = _data.Speed + ((_data.SpeedPerWave * _waveData.CurrentWave)/2);
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * (_speed * Time.deltaTime));
        
        Debug.Log("Enemy speed = " + _speed);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }

}
