using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{

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
            Dead();
        }
    }
    void Dead()
    {
        //the player is dead
        isDead = true;
        //Destroy player
        Destroy(gameObject);

    }
}
