using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollider : Collider
{

    void Start()
    {
        SetRb();
        objectTag = "PowerUp";
        SetupCollisionComponents(true, false, objectTag).size = new Vector3(1.2f, 1.2f, 1.2f);
    }

    void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {           
            Destroy(gameObject);
        }   
    }
}
