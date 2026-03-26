using UnityEngine;

public class Weapon : MonoBehaviour
{

    // Nouvelles variables pour les fonctionnalit�s demand�es
    [Header("Weapon Settings")]
    [SerializeField] int bulletCount = 1; // Nombre de projectiles tir�s simultan�ment
    [SerializeField] float bulletSpacing = 0.5f; // Espacement horizontal entre les projectiles
    [SerializeField] int maxBulletCount = 5; // Limite maximale de projectiles simultan�s

    protected float fireRate = 0.1f;
    float fireRateTimer;

    private void Update()
    {
        fireRateTimer += Time.deltaTime;


    }
    void FireWeapon()
    {
        if ( fireRateTimer < fireRate) return;
        fireRateTimer = 0;
        // Calcul de la position de d�part pour centrer les projectiles
        float startX = -((bulletCount - 1) * bulletSpacing) / 2;

        // Cr�ation de plusieurs balles c�te � c�te
        for (int i = 0; i < bulletCount; i++)
        {
            // Calcule la position avec l'offset horizontal
            Vector3 bulletOffset = new Vector3(startX + (i * bulletSpacing), -0.5f, 0.5f);
            Vector3 spawnPosition = transform.position + bulletOffset;

            // Instanciation du projectile
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            // Configuration des composants de collision pour la balle
            // Les projectiles doivent avoir un Rigidbody pour les collisions
            SetupCollisionComponents(bullet, true, false, "Bullet");

            // Ajouter le script de gestion de collision � la balle
            bullet.AddComponent<BulletCollider>();

            bullets.Add(bullet);
        }

        // Son de tir
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
