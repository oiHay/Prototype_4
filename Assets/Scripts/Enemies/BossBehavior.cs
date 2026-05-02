using System;
using System.Collections;
using UnityEngine;

public class BossBehavior : EnemyBehaviour
{
    [Header("Target & Power values")]
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float targetChaseTime;
    [SerializeField] private float targetLockTime;

    [Header("Effect values")] 
    [SerializeField] private float upwardTime;
    [SerializeField] private float force;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;

    private bool isBossAttacking = false;
    private Vector3 smashTargetPosition;

    private void Start()
    {
        StartCoroutine(AttackCycle());
    }

    protected override void Update()
    {
        if(!isBossAttacking)
            base.Update();
    }

    private IEnumerator AttackCycle()
    {
        while (true)
        {
            yield return StartCoroutine(ChasePlayer());
            yield return StartCoroutine(TargetChase());
            yield return StartCoroutine(SmashRoutine());
        }
    }

    private IEnumerator ChasePlayer()
    {
        isBossAttacking = false;
        yield return new WaitForSeconds(attackCooldown);

        isBossAttacking = true;
        EnemyRb.linearVelocity = new Vector3(0, EnemyRb.linearVelocity.y, 0);
    }

    private IEnumerator TargetChase()
    {
        GameObject instance = Instantiate(targetPrefab);
        instance.transform.position = new Vector3(Player.position.x, 0f, Player.transform.position.z);

        float chaseTimer = 0f;
        while (chaseTimer < targetChaseTime)
        {
            Vector3 targetPos = new Vector3(Player.position.x, 0f, Player.transform.position.z);

            instance.transform.position =
                Vector3.Lerp(instance.transform.position, targetPos, targetSpeed * Time.deltaTime);

            chaseTimer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(targetLockTime);

        smashTargetPosition = instance.transform.position;
        Destroy(instance);
    }

    private IEnumerator SmashRoutine()
    {
        float posY = transform.position.y;
       
        float jumpTime = Time.time + upwardTime;
        while (Time.time < jumpTime)
        {
            EnemyRb.linearVelocity = new Vector3(EnemyRb.linearVelocity.x, force, EnemyRb.linearVelocity.z);
            yield return null;
        }

        while (transform.position.y > posY)
        {
            Vector3 horizontalTarget = new Vector3(smashTargetPosition.x, transform.position.y, smashTargetPosition.z);
            Vector3 direction = (horizontalTarget - transform.position).normalized;
            
            
            EnemyRb.linearVelocity = new Vector3(
                direction.x * force, 
                -force * 2, 
                direction.z * force
                );
            yield return null;
        }

        PlayerController[] playerController = FindObjectsByType<PlayerController>();
        foreach (PlayerController player in playerController)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb)
                playerRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f, ForceMode.Impulse);
        }

        isBossAttacking = false;
    }
}
