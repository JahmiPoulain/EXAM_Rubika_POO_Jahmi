using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollider : Collider
{
    // Start is called before the first frame update
    void Start()
    {
        SetupCollisionComponents(true, false, "PowerUp");
    }

   /* // Update is called once per frame
    void Update()
    {
        
    }*/
}
