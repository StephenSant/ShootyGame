using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heeelth : MonoBehaviour {

    int curHealth;
    int maxHealth = 100;
    bool isDead = false;
	// Use this for initialization
	void Start ()
    {
        isDead = false;
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            curHealth = 0;
            Dead();
        }
    }
    void Dead()
    {
        isDead = true;

        Destroy(gameObject);
    }
}
