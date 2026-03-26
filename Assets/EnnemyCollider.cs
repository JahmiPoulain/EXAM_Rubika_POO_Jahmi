/*// Script pour les ennemis
using UnityEngine;

public class EnemyCollider : MonoBehaviour
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
            // Le joueur a touché un ennemi
            gameManager.HandlePlayerHit(gameObject);
        }
    }
}*/