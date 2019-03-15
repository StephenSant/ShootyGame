using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;
    public bool isDead;
	// Use this for initialization
	void Start ()
    {
        isDead = false;
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
	}
    public void TakeDamage(int damage)
    {
        //current health is minused by damage
        curHealth -= damage;
        //if curhealth is less then or equal to 0
        if(curHealth <= 0)
        {
            curHealth = 0;
            //you are dead, no big suprise
            Dead();
        }
    }
    public void Dead()
    {
        //the player is dead
        isDead = true;
        //Destroy player
        Destroy(gameObject);

    }
}
