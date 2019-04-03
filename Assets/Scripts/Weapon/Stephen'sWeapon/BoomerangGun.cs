using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangGun : Weapon
{
    public GameObject boomerangPrefab;
    public GameObject boomerang;

    public override void PrimaryFire()
    {
        if (boomerang == null)
        {
            boomerang = Instantiate(boomerangPrefab, firePoint.position, firePoint.rotation);
        }
    }
    public override void SecondaryFire()
    {
        boomerang.GetComponent<Boomerang>().isReturning = true;
    }
    private void Update()
    {
        if (boomerang != null)
        {
            boomerang.GetComponent<Boomerang>().returnPoint = transform.parent.parent.parent.transform;
        }

    }


}
