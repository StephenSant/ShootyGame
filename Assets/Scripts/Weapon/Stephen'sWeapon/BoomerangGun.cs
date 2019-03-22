using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangGun : Weapon
{
    public GameObject boomerang;

    public override void PrimaryFire()
    {
        Instantiate(boomerang, firePoint.position, firePoint.rotation);
    }

}
