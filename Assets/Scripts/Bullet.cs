using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float bulletSpeed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // R�initialiser la v�locit� et appliquer une nouvelle force
            rb.linearVelocity = Vector3.forward * bulletSpeed;
        }   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb == null)
        {
            // Fallback au mouvement par transform si pas de Rigidbody
            transform.position += Vector3.forward * bulletSpeed * Time.deltaTime;
        }
            // Suppression des balles qui sortent de l'�cran
            if (transform.position.z > 9) // Chang� de y � z
        {
            Destroy(gameObject);
            //bullets.RemoveAt(i);
        }
    }
}
