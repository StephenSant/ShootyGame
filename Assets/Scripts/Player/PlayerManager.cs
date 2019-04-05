using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour//Stephen
{
    [Header("References")]
    public PlayerMovement movement;
    public PlayerLook look;
    public Transform weaponPos;
    public PlayerHealth playerHealth;
    public GameObject playerMesh;
    public Collider playerCollider;
    public Camera firstPersonCamera;

    GameObject gun; //weapon held as a gameobject
    Weapon weapon; //will grab the weapon type script from the gun


    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        playerHealth = GetComponent<PlayerHealth>();
        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        UI.instance.healthBar.value = playerHealth.curHealth;

        if (weapon != null)
        {
            if (Input.GetMouseButton(0))
            {
                weapon.PrimaryFire();
            }
            if (Input.GetMouseButton(1))
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
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapon == null)
        {
            gun = (GameObject)Instantiate(Resources.Load("SMG"), weaponPos);
            weapon = gun.GetComponent<Weapon>();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            weapon = null;
            Destroy(gun);
        }
        if (Input.GetKeyDown(KeyCode.R) && weapon != null)
        {
            weapon.Reload();
        }
    }
    public void Dead()
    {
        playerHealth.isDead = true;
        playerMesh.SetActive(false);
        gun = null;
        weapon = null;
        playerCollider.enabled = false;
        movement.enabled = false;
        look.enabled = false;
        firstPersonCamera.GetComponent<DeathCam>().enabled = true;
        UI.instance.hudPanel.SetActive(false);
    }


}
