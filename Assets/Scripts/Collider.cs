using UnityEngine;

public class Collider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false; // D�sactiver la gravit� pour un jeu spatial
            rb.isKinematic = false; // Ne pas rendre kin�matique pour permettre les collisions physiques
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; // Figer certains axes
            rb.interpolation = RigidbodyInterpolation.Extrapolate;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    protected BoxCollider SetupCollisionComponents(bool hasRigidbody, bool isTrigger, string tag)
    {
        // Ajouter ou configurer le collider si n�cessaire
        BoxCollider boxCollider = GetComponent<BoxCollider>();
          if (boxCollider == null)
          {
            // Ajouter un BoxCollider par d�faut
            boxCollider = gameObject.AddComponent<BoxCollider>();

              // Ajuster la taille du collider en fonction du tag

             /* if (tag == "Bullet")
              {
                  // Collider plus petit pour les balles
                  boxCollider.size = new Vector3(0.3f, 0.3f, 0.5f);
              }
              else if (tag == "PowerUp")
              {
                  // Collider plus grand pour les power-ups pour faciliter leur collecte
                  boxCollider.size = new Vector3(1.2f, 1.2f, 1.2f);
              }*/
          }

        // Configurer le collider comme trigger ou non
        boxCollider.isTrigger = isTrigger;
         
          // D�finir le tag
          gameObject.tag = tag;
        return boxCollider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
