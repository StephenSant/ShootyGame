using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerLook))]
public class PlayerManager : MonoBehaviour
{
   
    [Header("Health")]
    public int maxHealth = 100;
    public int curHealth;
    [Header ("References")]
    public PlayerMovement movement;
    public PlayerLook look;

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
    void Update () {
		
	}
}
