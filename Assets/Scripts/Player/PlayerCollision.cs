using System;
using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float _forceStrength = 10f;
    private bool _hasPowerUp;
    private PowerUpEffect _activeEffect;
    private Coroutine _powerUpCoroutine;
    private GameObject _currentIndicator;

    #region PowerUpEffects

    public void EnableForce(float strength, GameObject indicatorPrefab)
    {
        _hasPowerUp = true;
        _forceStrength = strength;

        if (indicatorPrefab == null)
        {
            Debug.Log( "Indicator prefab não atribuido no ForceEffect");
            return;
        }
        
        _currentIndicator = Instantiate(indicatorPrefab, transform.position + new Vector3(0, -0.5f, 0),
            Quaternion.identity);
        _currentIndicator.SetActive(true);
        _currentIndicator.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    public void DisableForce()
    {
        _hasPowerUp = false;
        _forceStrength = 10f;

        if (_currentIndicator != null)
        {
            Destroy(_currentIndicator);
        }
    }

    #endregion

    private void Update()
    {
        if (_currentIndicator != null)
        {
            _currentIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }
        
        Debug.Log(_forceStrength);
    }

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
