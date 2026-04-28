using System;
using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float forceStrength = 10f;
    private bool hasPowerUp;
    private PowerUpEffect activeEffect;
    private Coroutine powerUpCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PowerUp")) return;

        PowerUp powerUp = other.GetComponent<PowerUp>();
        if (powerUp == null) return;

        if (powerUpCoroutine != null)
        {
            activeEffect?.Deactivate(gameObject);
            StopCoroutine(powerUpCoroutine);
        }

        activeEffect = powerUp.Data.Effect;
        activeEffect.Activate(gameObject);
        powerUpCoroutine = StartCoroutine(PowerUpCountdownRoutine(activeEffect));

        Destroy(other.gameObject);
    }

    IEnumerator PowerUpCountdownRoutine(PowerUpEffect effect)
    {
        yield return new WaitForSeconds(effect.Duration);
        effect.Deactivate(gameObject);
        activeEffect = null;
    }

    public void ForceEndPowerUp()
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
            powerUpCoroutine = null;
        }
        
        activeEffect?.Deactivate(gameObject);
        activeEffect = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") || !hasPowerUp) return;

        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        if (enemyRb == null) return;

        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
        enemyRb.AddForce(awayFromPlayer * forceStrength, ForceMode.Impulse);
    }

    public void SetForce(bool active, float strength = 10f)
    {
        hasPowerUp = active;
        forceStrength = strength;
    }
}
