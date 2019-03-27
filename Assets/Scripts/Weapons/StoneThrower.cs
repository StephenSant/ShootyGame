using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrower : MonoBehaviour
{
    public float rateOfFire = 10f;
    public GameObject projectile;
    public int curAmmo;
    public int ammo;
    public Transform muzzle;
    public int pellets = 8;
    public GameObject wall;
    public float pelletSpread = 2.5f;
    public void Start()
    {
        
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    public void Attack()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = transform.forward;

            Vector3 spread = Vector3.zero;

            spread += transform.up * Random.Range(-pelletSpread, pelletSpread);

            spread += transform.right * Random.Range(-pelletSpread, pelletSpread);

            GameObject clone = Instantiate(projectile, muzzle.position, muzzle.rotation);

            ShotgunProjectiles pellet = clone.GetComponent<ShotgunProjectiles>();

            pellet.Fire(direction + spread);

            
        }
    }
}
