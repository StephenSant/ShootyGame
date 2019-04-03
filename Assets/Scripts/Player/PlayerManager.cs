using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerLook))]
public class PlayerManager : MonoBehaviour//Stephen
{

    [Header("Health")]
    public int maxHealth = 100;
    public int curHealth;
    [Header("References")]
    public PlayerMovement movement;
    public PlayerLook look;
    public Weapon weapon;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

    }

    private void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.PrimaryFire();
        }
        if (Input.GetMouseButtonDown(1))
        {
            weapon.SecondaryFire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && weapon == null)
        {
            Instantiate(Resources.Load("BoomerangGun"));
        }
    }
}
