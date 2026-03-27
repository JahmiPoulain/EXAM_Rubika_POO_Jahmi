using UnityEngine;

public class DangerCollider : Collider
{
    protected GameManager gameManager;

   /* void Start()
    {
       // SetRb();
       // gameManager = FindFirstObjectByType<GameManager>();
        //SetupCollisionComponents(true, false, objectTag);
        //if (tag != null)
    }*/

    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (gameManager == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Le joueur a touché un ennemi
            gameManager.HandlePlayerHit(gameObject);
        }
    }
}
