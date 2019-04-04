using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))][ RequireComponent(typeof(PlayerLook))] [RequireComponent(typeof(Health))]
public class PlayerManager : MonoBehaviour//Stephen
{

    [Header("References")]
    public PlayerMovement movement;
    public PlayerLook look;
    public Transform weaponPos;
    public Health playerHealth;

    GameObject gun; //weapon held as a gameobject
    Weapon weapon; //will grab the weapon type script from the gun
    

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.PrimaryFire();
            }
            if (Input.GetMouseButtonDown(1))
            {
                weapon.SecondaryFire();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && weapon == null)
        {
            gun = (GameObject)Instantiate(Resources.Load("Boomerang Gun"), weaponPos);
            weapon = gun.GetComponent<Weapon>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            weapon = null;
            Destroy(gun);
            Instantiate(Resources.Load("BoomerangGun"));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.Reload();
        }
    }
}
