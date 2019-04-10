using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangGun : Weapon//stephen
{
    public GameObject boomerangPrefab;
    public GameObject boomerang;
    public int boomerangAmount = 1;
    public GameObject particleParent; 
    ParticleSystem[] particles = new ParticleSystem[3];
    public void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }
    public override void Start()
    {
        maxAmmo = boomerangAmount;
        curAmmo = maxAmmo;
    }

    public override void PrimaryFire()
    {
        if (boomerang == null)
        {
            for (int i = 0; i < 2; i++)
            {
                particles[i].Play();
            }
            boomerang = Instantiate(boomerangPrefab, firePoint.position, firePoint.rotation);
        }
    }
    public override void SecondaryFire()
    {
        if (boomerang != null)
        {
            boomerang.GetComponent<Boomerang>().isReturning = true;
        }
    }
    public override void Update()
    {
        if (boomerang != null)
        {
            boomerang.GetComponent<Boomerang>().returnPoint = transform.parent.parent.parent.transform;
            curAmmo = 0;
        }
        else
        {
            curAmmo = 1;
        }
        UIManager.instance.ammoText.text = "" + curAmmo+" / "+ maxAmmo;

    }


}
