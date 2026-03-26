// Script pour les projectiles
using UnityEngine;

public class BulletCollider : Collider
{
    private GameManager gameManager;
   // [SerializeField] Vector3 boxColliderSize;
    //[SerializeField] string objectTag;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        // Collider plus petit pour les balles
        SetupCollisionComponents(true,false, objectTag).size = new Vector3(0.3f, 0.3f, 0.5f);     
    }

    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (gameManager == null) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Balle touche ennemi
            gameManager.HandleBulletEnemyCollision(gameObject, collision.gameObject);
            gameManager.AddScore(100);

            // Chance de générer un power-up
            if (Random.value < 0.5f)
            {
                gameManager.SpawnPowerUp(collision.transform.position);
            }
            
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Balle touche astéroïde
            gameManager.HandleBulletEnemyCollision(gameObject, collision.gameObject);
            gameManager.AddScore(50);
        }
    }
} 