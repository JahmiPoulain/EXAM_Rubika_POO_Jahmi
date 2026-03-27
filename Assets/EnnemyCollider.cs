// Script pour les ennemis
using UnityEngine;

public class EnemyCollider : DangerCollider
{
    void Start()
    {
        objectTag = "Enemy";
        SetRb();
        gameManager = FindFirstObjectByType<GameManager>();
        SetupCollisionComponents(true, false, objectTag);
    }
}