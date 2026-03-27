// Script pour les projectiles
using UnityEngine;

public class BulletCollider : Collider
{
    private GameManager gameManager;
    [Header("Explosion")]
    public ExplosionManager explosionManager;
    public GameObject explosionPrefab;
    void Start()
    {
        SetRb();
        objectTag = "Bullet";
        gameManager = FindFirstObjectByType<GameManager>();
        explosionManager = FindFirstObjectByType<ExplosionManager>();
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
            gameManager.AddScore(100);
            
            // Chance de générer un power-up
            if (Random.value < 0.5f)
            {
                gameManager.SpawnPowerUp(collision.transform.position);
                
            }
            Collided(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Balle touche astéroïde
            gameManager.AddScore(50);
            Collided(collision.gameObject);
        }
    }

    void Collided(GameObject collided)
    {
        // Explosion avec effet de fragmentation
        if (explosionManager != null)
        {
            explosionManager.ExplodeObject(collided);
        }
        else
        {
            // Fallback vers l'explosion originale
            Instantiate(explosionPrefab, collided.transform.position, Quaternion.identity);
        }

        //Destruction de la balle
        Destroy(gameObject);
    }
} 