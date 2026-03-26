using UnityEngine;

// Le joueur peut se deplacer, prendre un coup et utiliser son arme
public class PlayerShip : MonoBehaviour
{
    // Références au GameManager pour accéder aux données
    private GameManager gameManager;
    private PlayerInputManager inputsManager;
    GameObject inputManager;
    Weapon weapon;
    // Variables dupliquées qui créent des dépendances
    public float speed;
    public int lives;

    void Start()
    {
        // Recherche du GameManager dans la scène
        gameManager = FindFirstObjectByType<GameManager>();

        // S'assurer que le joueur a les composants n�cessaires pour les collisions
        //SetupCollisionComponents(playerShip, true, false, "Player");

        // Ajouter le script de gestion de collision au joueur
        if (GetComponent<PlayerCollider>() == null)
        {
            gameObject.AddComponent<PlayerCollider>();
        }

        weapon = new Weapon();
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        // Initialisation des variables
        // speed = gameManager.playerSpeed;
        // lives = gameManager.lives;
    }

    void Update()
    {
        HandlePlayerInput();
        // Mise à jour des variables depuis le GameManager
        // speed = gameManager.playerSpeed;
        // lives = gameManager.lives;
    }

    void HandlePlayerInput()
    {

        // D�placement du joueur
        float horizontalInput = PlayerInputManager.instance.horizontalInput;
        float verticalInput = PlayerInputManager.instance.verticalInput;

        // D�placement sur le plan XZ
        Vector3 movement = PlayerInputManager.instance.movementInput * speed * Time.deltaTime;
        transform.position += movement;

        // Calcul des angles de rotation pour les deux axes
        float tiltAngleZ = -horizontalInput * 30f; // Inclinaison lat�rale (gauche/droite)
        float tiltAngleX = verticalInput * 15f;    // Inclinaison longitudinale (avant/arri�re)

        // Cr�ation d'une rotation qui combine les deux inclinaisons
        Quaternion targetRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);

        // Application de la rotation avec un lissage pour un effet plus naturel
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

        // Si aucun input, retour progressif � la rotation neutre
        if (horizontalInput == 0 && verticalInput == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 5f * Time.deltaTime);
        }

        // Limites de l'�cran pour le joueur
        Vector3 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -8.4f, 8.4f);
        playerPos.z = Mathf.Clamp(playerPos.z, -11, -2.5f);
        transform.position = playerPos;

        if (PlayerInputManager.instance.fireInput)
        {
            //FireBullet();        
        }
    }

    
}