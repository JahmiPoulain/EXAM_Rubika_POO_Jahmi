using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Nouvelles variables pour les fonctionnalit�s demand�es
    [Header("Weapon Settings")]
    [SerializeField] int bulletCount = 1; // Nombre de projectiles tir�s simultan�ment
    [SerializeField] float bulletSpacing = 0.5f; // Espacement horizontal entre les projectiles
    [SerializeField] int maxBulletCount = 5; // Limite maximale de projectiles simultan�s

    private List<GameObject> bullets = new List<GameObject>();

    protected float fireRate = 0.1f;
    float fireRateTimer;

   // public TMPro.TMP_Text powerupMessageText; // Pour afficher les messages de powerup
    private void Start()
    {
       //if (powerupMessageText) powerupMessageText.gameObject.SetActive(false);
    }
    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        if (PlayerInputManager.instance.fireInput)
        {
            Debug.Log("FIRED");
            FireWeapon();
        }
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
            bullets.Clear();
        }

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

    public void ApplyPowerUp()
    {
        // Augmenter le nombre de projectiles pour tous les power-ups
        if (bulletCount < maxBulletCount)
        {
            bulletCount++;

            // Affichage d'un message temporaire pour informer le joueur
            StartCoroutine(GameManager.instance.ShowPowerupMessage("Weapon Upgraded! Bullets: " + bulletCount));
        }
        else
        {
            // Bonus de score si le joueur a d�j� le maximum de projectiles
            GameManager.instance.AddScore(200);
            StartCoroutine(GameManager.instance.ShowPowerupMessage("Max Weapon Level! +200 Score"));
        }
    }

   /* IEnumerator ShowPowerupMessage(string message)
    {
        if (powerupMessageText != null)
        {
            powerupMessageText.text = message;
            powerupMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            powerupMessageText.gameObject.SetActive(false);
        }
        yield return null;
    }*/
}
