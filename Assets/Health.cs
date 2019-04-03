using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
	}

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }

    private void Update()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
