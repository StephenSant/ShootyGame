using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : Health
{

    // Use this for initialization
    protected override void Start()
    {
        maxHealth = 200;
        curHealth = maxHealth;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Dead();
        }
    }
    public override void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
    protected void Dead()
    {
        Destroy(gameObject);
    }
}
