using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float curHealth;
    public float maxHealth = 200f;
	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Dead();
        }
	}
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }
    void Dead()
    {
        Destroy(gameObject);
    }
}
