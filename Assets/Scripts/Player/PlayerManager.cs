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

        UIManager.instance.healthBar.value = playerHealth.curHealth;

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
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapon == null)
        {
            gun = (GameObject)Instantiate(Resources.Load("Gun"), weaponPos);
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
            
            weapon.canShoot = false;
            
        }
    }
    public void Dead()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerHealth.isDead = true;
        playerMesh.SetActive(false);
        gun = null;
        weapon = null;
        playerCollider.enabled = false;
        movement.enabled = false;
        look.enabled = false;
        firstPersonCamera.GetComponent<DeathCam>().enabled = true;
        UIManager.instance.hudPanel.SetActive(false);
        UIManager.instance.deathPanel.SetActive(true);
    }
     public void Respawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstPersonCamera.transform.position = Vector3.zero;
        playerHealth.curHealth = playerHealth.maxHealth;
        transform.position = new Vector3(-1, -8, -9);
        playerMesh.SetActive(true);
        playerCollider.enabled = true;
        movement.enabled = true;
        look.enabled = true;
        firstPersonCamera.GetComponent<DeathCam>().enabled = false;
        UIManager.instance.hudPanel.SetActive(true);
        UIManager.instance.deathPanel.SetActive(false);
        playerHealth.isDead = false;
    }

}
