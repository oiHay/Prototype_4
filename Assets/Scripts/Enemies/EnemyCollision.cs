using System;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private EnemyBehaviour _behaviour;

    private void Awake()
    {
        _behaviour = GetComponent<EnemyBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bounder"))
        {
            _behaviour.HandleOutOfBounder();
        }
    }
}
