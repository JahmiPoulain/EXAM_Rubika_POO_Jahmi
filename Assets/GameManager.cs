// Le fichier GameManager.cs - Une classe monolithique qui fait tout
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
   // [Header("Explosion")]
   // public ExplosionManager explosionManager;

    // R�f�rence directe � tous les objets du jeu
    public GameObject playerShip;
  //  public GameObject enemyPrefab;
  //  public GameObject asteroidPrefab;
   // public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject powerUpPrefab;

    // Variables publiques expos�es sans encapsulation
    int score;
    public int lives;
    public float playerSpeed = 5.0f;
    //public float bulletSpeed = 10.0f;
  //  public float enemySpeed = 3.0f;
  //  public float asteroidSpeed = 2.0f;
   // public float spawnRate = 2.0f;

    // Nouvelles variables pour les fonctionnalit�s demand�es
    //[Header("Weapon Settings")]
   // public int bulletCount = 1; // Nombre de projectiles tir�s simultan�ment
   // public float bulletSpacing = 0.5f; // Espacement horizontal entre les projectiles
   // public int maxBulletCount = 5; // Limite maximale de projectiles simultan�s

    [Header("Difficulty Settings")]
   // public float initialSpawnRate = 2.0f; // Taux de spawn initial
   // public float minSpawnRate = 0.5f; // Taux de spawn minimal (plus difficile)
   // public float spawnRateDifficulty = 0.1f; // R�duction du taux de spawn par minute
    private float gameTime = 0f; // Temps de jeu �coul�
    public float minutesPlayed { get; private set; }

    // Listes pour suivre tous les objets du jeu
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> asteroids = new List<GameObject>();
   // private List<GameObject> bullets = new List<GameObject>();
    public List<GameObject> powerUps = new List<GameObject>();

    // Variables pour le timing
    private float nextSpawnTime;

    // UI references
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text livesText;
    public GameObject gameOverPanel;
    //public TMPro.TMP_Text powerupMessageText; // Pour afficher les messages de powerup
    public TMPro.TMP_Text timeText; // Pour afficher le temps �coul�
    public GameObject playerDamageEffect; // Effet visuel quand un ennemi traverse

    public bool isGameOver { get; private set; }
    private float restartCountdown = 3.0f;
    public TMPro.TMP_Text countdownText;

    // Avant de remplacer le syst�me de collisions, il faut cr�er des classes pour g�rer les collisions
    // Ces classes seront attach�es aux objets du jeu concern�s

    // Voici les scripts � cr�er pour le syst�me de trigger/collision Unity
    // Note pour les �tudiants : Ces scripts devraient �tre dans des fichiers s�par�s pour respecter les principes SOLID




    //bool restartGame;
    public TMPro.TMP_Text powerupMessageText; // Pour afficher les messages de powerup




    // M�thode pour g�rer les collisions avec le joueur
    public void HandlePlayerHit(GameObject hitObject)
    {
        // Destruction de l'objet qui a touch� le joueur
        Instantiate(explosionPrefab, hitObject.transform.position, Quaternion.identity);

        if (hitObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            enemies.Remove(hitObject);
            //Destroy(hitObject);
            
        }
        else if (hitObject.CompareTag("Asteroid"))
        {
            Debug.Log("Hit Asteroid");
            asteroids.Remove(hitObject);
            //Destroy(hitObject);
            Debug.Log(hitObject);
            
        }

        // Perte d'une vie
        lives--;
        Debug.Log(lives);
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        isGameOver = false;
        // Initialisation
        score = 0;
        lives = 3;
  //      bulletCount = 1;
        gameTime = 0f;
      //  spawnRate = initialSpawnRate;
       // nextSpawnTime = Time.time + spawnRate;
        UpdateUI();
        if (gameOverPanel) gameOverPanel.SetActive(false);
        // if (powerupMessageText) powerupMessageText.gameObject.SetActive(false);

        if (powerupMessageText) powerupMessageText.gameObject.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Augmentation du temps de jeu
            gameTime += Time.deltaTime;

            // Calcul du nouveau taux de spawn en fonction du temps �coul� (en minutes)
            minutesPlayed = gameTime / 2f;
            // spawnRate = Mathf.Max(minSpawnRate, initialSpawnRate - (spawnRateDifficulty * minutesPlayed));

            // Affichage du temps de jeu (optionnel)
            if (timeText != null)
            {
                int minutes = Mathf.FloorToInt(gameTime / 60);
                int seconds = Mathf.FloorToInt(gameTime % 60);
                timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
            }

            // Mise � jour de l'UI
            UpdateUI();
            if (lives <= 0)
            {
                GameOver();
            }
        }
        
        // Gestion du d�compte de red�marrage
        if (isGameOver)
        {
            restartCountdown -= Time.deltaTime;

            // Mise � jour du texte avec la valeur arrondie � l'entier sup�rieur
            if (countdownText != null)
            {
                countdownText.text = "Red�marrage dans: " + Mathf.Ceil(restartCountdown).ToString();
            }
            Debug.Log(restartCountdown);
            // Lorsque le d�compte atteint z�ro
            if (restartCountdown <= 0)
            {
                //Debug.Log(restartCountdown);
               // restartGame = true;
                RestartGame();
            }

        }
    }

    public void RemoveLife()
    {
        lives--;
    }

    public void SpawnPowerUp(Vector3 position)
    {
        GameObject powerUp = Instantiate(powerUpPrefab, position, Quaternion.identity);

        // Configuration des composants de collision pour le power-up
        //SetupCollisionComponents(powerUp, true, false, "PowerUp");

        // Ajouter le script de gestion de collision au power-up
        powerUp.AddComponent<PowerUpCollider>();

        powerUps.Add(powerUp);
    }

    // Coroutine pour afficher un message temporaire
    public IEnumerator ShowPowerupMessage(string message)
    {
       if (powerupMessageText != null)
        {
            powerupMessageText.text = message;
            powerupMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            powerupMessageText.gameObject.SetActive(false);
        }
        yield return null;
    }

    void UpdateUI()
    {
        // Mise � jour des textes de score et de vies
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    void GameOver()
    {
        // Affichage du panel de game over
        gameOverPanel.SetActive(true);

        // Initialisation du compte � rebours
        isGameOver = true;
        restartCountdown = 3.0f;

        // Mise � jour initiale du texte de d�compte
        if (countdownText != null)
        {
            countdownText.text = "Red�marrage dans: " + Mathf.Ceil(restartCountdown).ToString();
            countdownText.gameObject.SetActive(true);
        }

        // Note: ne pas arr�ter le temps ici puisque nous voulons que le d�compte fonctionne
        // Time.timeScale = 0; -- retirez cette ligne s'il elle est pr�sente
    }

    public void RestartGame()
    {
        // il y a un bug qui fait que l'ennemi uniquement (pas l'astéroide) ne subit pas les dégats des bullets quand on RestartGame()
        // pour que ca marche bien je fais juste un reload de la scene ( c'est honteux )
        SceneManager.LoadScene("SampleScene");

        // R�initialisation du statut de game over
        isGameOver = false;

        // Masquage du texte de d�compte
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        // Remise � z�ro du jeu
        Time.timeScale = 1;
      
        foreach (GameObject powerUp in powerUps)
        {
            Destroy(powerUp);
        }
        powerUps.Clear();

        // R�initialisation des variables
        score = 0;
        lives = 3;
        gameTime = 0f;

        // Masquage du panel de game over
        gameOverPanel.SetActive(false);

        // Replacement du joueur
        playerShip.transform.position = new Vector3(0, 0, -7);
        playerShip.transform.rotation = Quaternion.identity;
    }
}