using System;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [Header("Target & Power values")]
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;
    private bool isPlayerLocated = false;

    [Header("Effect values")] 
    [SerializeField] private float upwardTime;
    [SerializeField] private float force;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    // 1. cooldown começa a recontar, boss anda atrás do player  - IEnumerator e Coroutine
    // 2. cooldown recarrega
    // 3. parar de andar e procurar o player com o target; boss speed = 0; show targetPrefab - target precisa de rigidbody? 
    // 4. marcar um lugar depois de x tempo; targetPrefab speed = 0; player located = true - outra coroutine?
    // 5. ataque começa, vulgo smashEffect do boss - ver como funciona smashBehavior do player
    // 6. cooldown começa a recontar, boss anda atrás do player - Coroutine de cooldown começa novamente
    
}
