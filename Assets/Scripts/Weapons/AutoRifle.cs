using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : MonoBehaviour
{
    public float fireRate = 1.5f;
    public float range = 10f;
    public GameObject bulletPrefab;
    public Transform muzzle;
    public float timetoFire;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
	}
    void Shoot()
    {
        GameObject clone = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }
    //IEnumerator RateOfFire()
    //{

    //}
}
