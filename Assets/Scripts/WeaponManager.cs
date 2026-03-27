using UnityEngine;
using UnityEngine.WSA;

public class WeaponManager : MonoBehaviour
{
    // on a un PlayerShip par défaut
    Transform playerShip;
    [Header("Weapon")]
    // le tableau de toutes les armes du jeu, si on en veux d'autres
    public GameObject[] weaponsPrefabs;
    GameObject equipedWeapon;
    
    void Start()
    {
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
        // weaponScript = equipedWeapon.GetComponent<Weapon>();
    }
}
