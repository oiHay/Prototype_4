using System;
using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float _forceStrength = 10f;
    private bool _hasPowerUp;
    private PowerUpEffect _activeEffect;
    private Coroutine _powerUpCoroutine;

    private ProjectileShooter shooter;
    private PowerUpIndicator indicator;

    private void Awake()
    {
        shooter = GetComponent<ProjectileShooter>();
        indicator = GetComponent<PowerUpIndicator>();
    }

    #region PowerUp Effect
    public void EnableForce(float strength, GameObject indicatorPrefab)
    {
        _hasPowerUp = true;
        _forceStrength = strength;
        indicator.Show(indicatorPrefab, new Vector3(0,- 0.5f, 0), new Vector3(2.5f, 2.5f, 2.5f));
    }

    public void DisableForce()
    {
        _hasPowerUp = false;
        _forceStrength = 10f;
        indicator.Hide();
    }

    public void EnableGun(float speed, float fireInterval, GameObject projectilePrefab, GameObject indicatorPrefab)
    {
        _hasPowerUp = true;
        shooter.StartShooting(speed, fireInterval, projectilePrefab);
        indicator.Show(indicatorPrefab, new Vector3(0,- 0.5f, 0), new Vector3(2.5f, 2.5f, 2.5f));
    }

    public void DisableGun()
    {
        _hasPowerUp = false;
        shooter.StopShooting();
        indicator.Hide();
    }
    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("PowerUp")) return;

        PowerUp powerUp = other.GetComponent<PowerUp>();
        if(powerUp ==null) return;

        if (_powerUpCoroutine != null)
        {
            _activeEffect?.Deactivate(gameObject);
            StopCoroutine(_powerUpCoroutine);
        }

        _activeEffect = powerUp.Data.Effect;
        _activeEffect.Activate(gameObject);
        _powerUpCoroutine = StartCoroutine(PowerUpCountdownRoutine(_activeEffect));
        
        Destroy(other.gameObject);
    }
    
    IEnumerator PowerUpCountdownRoutine(PowerUpEffect effect) // Coroutine para determinar quanto tempo o power up tem de duração
    {
        yield return new WaitForSeconds(effect.Duration); // o código espera x segundos para então estabelecer os dados abaixo
        effect.Deactivate(gameObject);
        _activeEffect = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") || !_hasPowerUp) return;
        
        Rigidbody _enemyRb = collision.gameObject.GetComponent<Rigidbody>(); // Pega o rigidbody do inimigo
        if(_enemyRb == null) return;
        
        Vector3 _awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized; // Ve qual a direção que o inimigo terá após impacto
        _enemyRb.AddForce(_awayFromPlayer * _forceStrength, ForceMode.Impulse); // Determina que se o jogador colidir com o mesmo, ele será "arremessado" em direção oposta a partir de uma força x
    }
}
