using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{

    public int maxHealth = 100;
    public int curHealth;
    public bool isDead;
    // Use this for initialization
    public virtual void Start ()
    {
        curHealth = maxHealth;
	}

    public virtual void TakeDamage(int damage)
    {
        curHealth -= damage;
    }

    public virtual void Update()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            curHealth = 0;
            Die();
        }
    }
    protected virtual void Die()
    {
        //the player is dead
        isDead = true;
        //Destroy player
        Destroy(gameObject);
    }
}
