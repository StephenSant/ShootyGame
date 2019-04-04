﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))][ RequireComponent(typeof(PlayerLook))] [RequireComponent(typeof(Health))]
public class PlayerManager : MonoBehaviour//Stephen
{

    [Header("References")]
    public PlayerMovement movement;
    public PlayerLook look;
    public Transform weaponPos;
    public PlayerHealth playerHealth;

    GameObject gun; //weapon held as a gameobject
    Weapon weapon; //will grab the weapon type script from the gun
    

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        UI.instance.healthBar.value = playerHealth.curHealth;

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
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapon == null)
        {
            gun = (GameObject)Instantiate(Resources.Load("StoneThrower"), weaponPos);
            weapon = gun.GetComponent<Weapon>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            weapon = null;
            Destroy(gun);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.Reload();
            
            weapon.canShoot = false;
            
        }
    }
}
