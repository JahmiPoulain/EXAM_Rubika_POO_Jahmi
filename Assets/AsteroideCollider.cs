/*// Script pour les astéroïdes
using UnityEngine;

public class AsteroidCollider : Collider
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (gameManager == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Le joueur a touché un astéroïde
            gameManager.HandlePlayerHit(gameObject);
        }
    }
}*/