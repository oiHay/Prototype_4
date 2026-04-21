using System;
using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject _powerUpIndicator;
    
    [SerializeField] private bool _hasPowerUp;
    
    [SerializeField] private float _powerUpCooldown = 7.0f;
    [SerializeField] private float _powerUpStrength = 15.0f;

    private void Start()
    {
        _powerUpIndicator.transform.SetParent(null); // Ao começar a leitura do script, _powerUpIndicator, que é filho do player, tem seu parentesco zerado, o que permite que o mesmo não gire quando o player anda 
    }

    private void Update()
    {
        _powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            _hasPowerUp = true;
            _powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    IEnumerator PowerUpCountdownRoutine() // Coroutine para determinar quanto tempo o power up tem de duração
    {
        yield return new WaitForSeconds(_powerUpCooldown); // o código espera x segundos para então estabelecer os dados abaixo
        DeactivatePowerUp();
    }

    private void DeactivatePowerUp()
    {
        _hasPowerUp = false; // no caso, determina que o jogador não tem power up, retirando os benefícios do mesmo do jogador
        _powerUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _hasPowerUp)
        {
            Rigidbody _enemyRb = collision.gameObject.GetComponent<Rigidbody>(); // Pega o rigidbody do inimigo
            Vector3 _awayFromPlayer = (collision.gameObject.transform.position - transform.position); // Ve qual a direção que o inimigo terá após impacto
            
            _enemyRb.AddForce(_awayFromPlayer * _powerUpStrength, ForceMode.Impulse); // Determina que se o jogador colidir com o mesmo, ele será "arremessado" em direção oposta a partir de uma força x
            Debug.Log("Collided with: " + collision.gameObject.name + "with power up set to: " + _hasPowerUp);
        }
    }
}
