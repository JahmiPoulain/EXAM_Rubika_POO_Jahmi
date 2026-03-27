// Script pour les ennemis
using UnityEngine;

public class EnemyCollider : DangerCollider
{
   // private GameManager gameManager;

    void Start()
    {
        objectTag = "Enemy";
        SetRb();
        gameManager = FindFirstObjectByType<GameManager>();
        SetupCollisionComponents(true, false, objectTag);
        // gameManager = FindFirstObjectByType<GameManager>();
        // SetupCollisionComponents(true, false, "Enemy");
    }

 /*   // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (gameManager == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            // Le joueur a touché un ennemi
            gameManager.HandlePlayerHit(gameObject);
        }
    }*/
}