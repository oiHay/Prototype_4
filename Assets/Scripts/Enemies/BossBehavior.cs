using System;
using System.Collections;
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

    private Rigidbody bossRb;
    private float posY;
    private bool isReady;
    
    private Transform playerTransform;

    private void Awake()
    {
        bossRb = GetComponent<Rigidbody>(); 
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        StartCoroutine(SmashCooldown());
    }

    private void Update()
    {
        OnPowerActivation();
    }

    private void OnPowerActivation()
    {
        if(!isReady) return;
        isReady = false;
        StartCoroutine(SmashRoutine());
    }

    private IEnumerator SmashCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isReady = true;
    }

    private IEnumerator SmashRoutine()
    {
        posY = transform.position.y;
       
        float jumpTime = Time.time + upwardTime;

        while (Time.time < jumpTime)
        {
            bossRb.linearVelocity = new Vector2(bossRb.linearVelocity.x, force);
            yield return null;
        }

        while (transform.position.y > posY)
        {
            bossRb.linearVelocity = new Vector2(bossRb.linearVelocity.x, -force * 2);
            yield return null;
        }

        PlayerController[] playerController = FindObjectsByType<PlayerController>();
        foreach (PlayerController player in playerController)
        {
            if (!player) continue;

            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb)
            {
                playerRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f, ForceMode.Impulse);
            }
        }

        StartCoroutine(SmashCooldown());
    }
    
    // 1. cooldown começa a recontar, boss anda atrás do player  - IEnumerator e Coroutine
    // 2. cooldown recarrega
    // 3. parar de andar e procurar o player com o target; boss speed = 0; show targetPrefab - target precisa de rigidbody? 
    // 4. marcar um lugar depois de x tempo; targetPrefab speed = 0; player located = true - outra coroutine?
    // 5. ataque começa, vulgo smashEffect do boss - ver como funciona smashBehavior do player
    // 6. cooldown começa a recontar, boss anda atrás do player - Coroutine de cooldown começa novamente
    
}
