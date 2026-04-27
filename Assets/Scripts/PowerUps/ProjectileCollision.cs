using System;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] private int forceStrength;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>(); // Pega o rigidbody do inimigo
        if(enemyRb == null) return;
        
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized; // Ve qual a direção que o inimigo terá após impacto
        enemyRb.AddForce(awayFromPlayer * forceStrength, ForceMode.Impulse); // Determina que se o jogador colidir com o mesmo, ele será "arremessado" em direção oposta a partir de uma força x
        
        Destroy(this.gameObject);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag("ProjBounder")) return;
        
        Destroy(this.gameObject);
    }
}
