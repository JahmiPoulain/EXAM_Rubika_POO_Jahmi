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
}