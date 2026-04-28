using System;
using System.Collections;
using UnityEngine;

public class SmashBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float posY;
    private bool isSmashing;
    public bool isReady;
    
    private float _upwardTime;
    private float _strength;
    private float _explosionForce;
    private float _explosionRadius;

    public event Action OnSmashComplete;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

   public void SetReady(float upwardTime, float strength, float explosionForce, float explosionRadius)
   {
       _upwardTime = upwardTime;
       _strength = strength;
       _explosionForce = explosionForce;
       _explosionRadius = explosionRadius;
       isReady = true;
   }

   public void SetReady(bool ready)
   {
       isReady = ready;
   }

   public void TrySmash()
   {
       if (!isReady || isSmashing) return;
       isSmashing = true;
       StartCoroutine(SmashRoutine());
   }

   private IEnumerator SmashRoutine()
   {
       posY = transform.position.y;
       
       float jumpTime = Time.time + _upwardTime;

       while (Time.time < jumpTime)
       {
           rb.linearVelocity = new Vector2(rb.linearVelocity.x, _strength);
           yield return null;
       }

       while (transform.position.y > posY)
       {
           rb.linearVelocity = new Vector2(rb.linearVelocity.x, -_strength * 2);
           yield return null;
       }

       EnemyBehaviour[] enemies = FindObjectsByType<EnemyBehaviour>();
       foreach (EnemyBehaviour enemy in enemies)
       {
           if (enemy == null) continue;

           Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
           if (enemyRb != null)
               enemyRb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 0f, ForceMode.Impulse);
       }

       isSmashing = false;
       OnSmashComplete?.Invoke();
   }
}
