using System.Collections.Generic;
using UnityEngine;

public class DangerManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> asteroids = new List<GameObject>();

    private List<Rigidbody> enemiesRb = new List<Rigidbody>();
    private List<Rigidbody> asteroidsRb = new List<Rigidbody>();

    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;
    // Variables pour le timing
    public float nextSpawnTime; //{ get; private set; }

    public float bulletSpeed = 10.0f;
    public float enemySpeed = 3.0f;
    public float asteroidSpeed = 2.0f;
    public float spawnRate = 2.0f;

    public float initialSpawnRate = 2.0f; // Taux de spawn initial
    public float minSpawnRate = 0.5f; // Taux de spawn minimal (plus difficile)
    public float spawnRateDifficulty = 0.1f; // R�duction du taux de spawn par minute
                                             //private float gameTime = 0f; // Temps de jeu �coul�
    public GameObject playerDamageEffect; // Effet visuel quand un ennemi traverse
    private void Start()
    {
        spawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + spawnRate;
       // gameTime = 0f;
    }
    private void Update()
    {
        if (GameManager.instance != null)
        {
            spawnRate = Mathf.Max(minSpawnRate, initialSpawnRate - (spawnRateDifficulty * GameManager.instance.minutesPlayed));
            if (GameManager.instance.isGameOver)
            {
                spawnRate = initialSpawnRate;
                nextSpawnTime = Time.time + spawnRate;
            }

            if (GameManager.instance.isGameOver)
            {
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy);
                }
                enemies.Clear();
                enemiesRb.Clear();
                foreach (GameObject asteroid in asteroids)
                {
                    Destroy(asteroid);
                }
                asteroids.Clear();
                asteroidsRb.Clear();

            }
        }
        SpawnEnemiesAndAsteroids();
        MoveEnemies();
        MoveAsteroids();
    }
    void SpawnEnemiesAndAsteroids()
    {
        if (Time.time > nextSpawnTime)
        {
            if (Random.value < 0.3f)
            {
                // Spawn d'un ennemi
                float randomX = Random.Range(-8f, 8f);
                // Position de spawn sur l'axe Z au lieu de Y
                Vector3 spawnPosition = new Vector3(randomX, 0, 9);
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                
                // Configuration des composants de collision pour l'ennemi
                //SetupCollisionComponents(enemy, true, false, "Enemy");

                // Ajouter le script de gestion de collision � l'ennemi
                if (!enemy.GetComponent<EnemyCollider>()) enemy.AddComponent<EnemyCollider>();

                enemies.Add(enemy);
                enemiesRb.Add(enemy.GetComponent<Rigidbody>());
            }
            else
            {
                // Spawn d'un ast�ro�de
                float randomX = Random.Range(-8f, 8f);
                // Position de spawn sur l'axe Z au lieu de Y
                Vector3 spawnPosition = new Vector3(randomX, 0, 9);
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

                // Configuration des composants de collision pour l'ast�ro�de
                //SetupCollisionComponents(asteroid, true, false, "Asteroid");

                // Ajouter le script de gestion de collision � l'ast�ro�de
                if (!asteroid.GetComponent<AsteroidCollider>()) asteroid.AddComponent<AsteroidCollider>();


                asteroids.Add(asteroid);
                asteroidsRb.Add(asteroid.GetComponent<Rigidbody>());
            }

            nextSpawnTime = Time.time + spawnRate;
        }
    }
    void MoveEnemies()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] != null)
            {
                // Utiliser le Rigidbody pour le mouvement
                Rigidbody rb = enemiesRb[i];//enemies[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Appliquer directement une v�locit� au Rigidbody
                    rb.linearVelocity = Vector3.back * enemySpeed;
                }
                else
                {
                    // Fallback au mouvement par transform si pas de Rigidbody
                    enemies[i].transform.position += Vector3.back * enemySpeed * Time.deltaTime;
                }

                // Les ennemis ne disparaissent qu'� z=-12 et enl�vent une vie
                if (enemies[i].transform.position.z < -12)
                {
                    // Enlever un point de vie au joueur
                    if(GameManager.instance != null) GameManager.instance.RemoveLife();

                    // Effet visuel pour montrer que l'ennemi a travers�
                    if (playerDamageEffect != null)
                    {
                        Instantiate(playerDamageEffect, enemies[i].transform.position, Quaternion.identity);
                    }

                    // Destruction de l'ennemi
                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    enemiesRb.RemoveAt(i);


                }
            }
            else
            {
                enemies.RemoveAt(i);
                enemiesRb.RemoveAt(i);
            }
        }
    }

    void MoveAsteroids()
    {
        for (int i = asteroids.Count - 1; i >= 0; i--)
        {
            if (asteroids[i] != null)
            {
                // Direction al�atoire pour chaque ast�ro�de
                float randomX = Random.Range(-0.5f, 0.5f);

                // Utiliser le Rigidbody pour le mouvement
                Rigidbody rb = asteroidsRb[i];//asteroids[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Appliquer directement une v�locit� au Rigidbody
                    rb.linearVelocity = new Vector3(randomX, 0, -1) * asteroidSpeed;

                    // Appliquer une rotation
                    asteroids[i].transform.Rotate(0, 30 * Time.deltaTime, 0);
                }
                else
                {
                    // Fallback au mouvement par transform si pas de Rigidbody
                    Vector3 movement = new Vector3(randomX, 0, -1) * asteroidSpeed * Time.deltaTime;
                    asteroids[i].transform.position += movement;
                    asteroids[i].transform.Rotate(0, 30 * Time.deltaTime, 0);
                }

                // Les ast�ro�des ne disparaissent qu'� z=-12 et enl�vent une vie
                if (asteroids[i].transform.position.z < -12)
                {
                    // Enlever un point de vie au joueur
                    if (GameManager.instance != null) GameManager.instance.RemoveLife();

                    // Effet visuel pour montrer que l'ast�ro�de a travers�
                    if (playerDamageEffect != null)
                    {
                        Instantiate(playerDamageEffect, asteroids[i].transform.position, Quaternion.identity);
                    }

                    // Destruction de l'ast�ro�de
                    Destroy(asteroids[i]);
                    asteroids.RemoveAt(i);
                    asteroidsRb.RemoveAt(i);
                }
            }
            else
            {
                asteroids.RemoveAt(i);
                asteroidsRb.RemoveAt(i);
            }
        }
    }
}
