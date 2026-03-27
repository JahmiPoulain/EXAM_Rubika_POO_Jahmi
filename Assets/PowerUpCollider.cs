using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollider : Collider
{
    //[SerializeField] Vector3 boxColliderSize;
    
    void Start()
    {
        SetRb();
        objectTag = "PowerUp";
        SetupCollisionComponents(true, false, objectTag).size = new Vector3(1.2f, 1.2f, 1.2f);
    }

   /* // Update is called once per frame
    void Update()
    {
        
    }*/
}
