using UnityEngine;

public abstract class DangerCollider : Collider
{
    protected GameManager gameManager;

  
    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (gameManager == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Le joueur a touché un ennemi
            //gameManager.HandlePlayerHit(gameObject);
           // Debug.Log("DESTROY");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
