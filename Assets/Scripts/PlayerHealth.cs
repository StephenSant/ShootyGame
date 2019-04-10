using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    PlayerManager manager;
    public bool isBot=false;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    // Use this for initialization
    public override void Start()
    {
        isDead = false;
        curHealth = maxHealth;
    }

    public override void TakeDamage(int damage)
    {
        //current health is minused by damage
        curHealth -= damage;

        //if curhealth is less then or equal to 0
        if (curHealth <= 0)
        {
            curHealth = 0;
            //you are dead, no big suprise
            if (!isDead)
            {
                if (isBot) { Destroy(gameObject); }
                else { manager.Dead();}
            }
        }
    }
}
