using UnityEngine;


public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    //public static event System.Action FireChange;
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    public Vector3 movementInput { get; private set; }
    public bool fireInput { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandlePlayerInput();
    }

    void HandlePlayerInput()
    {
        // D�placement du joueur
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // D�placement sur le plan XZ normalisé
        movementInput = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Tir
        fireInput = Input.GetKeyDown(KeyCode.Space);        
    }    
}
