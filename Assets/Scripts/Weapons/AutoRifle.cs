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
    public float shotDelay = 0.1f;
    public int magCap = 25;
    public int currentAmmo;
    public bool reloading;
    public int firedShots;
	// Use this for initialization
	void Start ()
    {
        currentAmmo = magCap;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0) && Time.time > timetoFire && currentAmmo > 0  && Time.timeScale == 1 && !reloading)
        {
            //Shoot();
            currentAmmo -= 1;
            firedShots += 1;
        }
	}
}
