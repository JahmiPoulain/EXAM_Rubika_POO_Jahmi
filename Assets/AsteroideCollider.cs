// Script pour les astéroïdes
using UnityEngine;

public class AsteroidCollider : DangerCollider
{
   // private GameManager gameManager;

    void Start()
    {
        objectTag = "Asteroid";
        SetRb();
        gameManager = FindFirstObjectByType<GameManager>();
        SetupCollisionComponents(true, false, objectTag);
        //gameManager = FindFirstObjectByType<GameManager>();
    }

    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
   /* void OnCollisionEnter(Collision collision)
    {
        if (gameManager == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Le joueur a touché un astéroïde
            gameManager.HandlePlayerHit(gameObject);
        }
    }*/
}