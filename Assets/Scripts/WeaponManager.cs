using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // on a un PlayerShip par défaut mais si on veut mettre une arme sur autre chose on peut le faire et l'arme fonctionnera toujours
    Transform playerShip;

    [Header("Weapon")]
    // le tableau de toutes les armes du jeu, si on en veux d'autres
    public GameObject[] weaponsPrefabs;
    GameObject equipedWeapon;
    
    void Start()
    {
        playerShip = FindAnyObjectByType<PlayerShip>().transform;
        if (playerShip != null) SwitchToWeapon(0, playerShip);        
    }

    void Update()
    {
        
    }

    void SwitchToWeapon(int ID, Transform holder)
    {
        // Système simple pour changer d'arme si on veut ajouter plusieurs types d'armes
        Destroy(equipedWeapon);
        equipedWeapon = Instantiate(weaponsPrefabs[ID], holder.position, Quaternion.identity);
        equipedWeapon.transform.SetParent(holder);
        equipedWeapon.transform.localPosition = Vector3.zero;
    }

    public void ApplyPowerUpToWeapon()
    {
        equipedWeapon.GetComponent<Weapon>().ApplyPowerUp();
    }
}
