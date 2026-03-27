using UnityEngine;

public abstract class Collider : MonoBehaviour
{
    [SerializeField] protected string objectTag;

    void Start()
    {
        SetRb();
    }

    protected void SetRb()
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
          }

        // Configurer le collider comme trigger ou non
        boxCollider.isTrigger = isTrigger;
         
          // D�finir le tag
        gameObject.tag = tag;
        return boxCollider;
    }
}
