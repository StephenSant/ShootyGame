using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 8;
    [Header("Shooting")]
    public int curAmmo;
    public int maxAmmo = 20;
    public float rateOfFire = 2.6f;
    public float reloadDelay = 1f;
    public bool canShoot = true;

    protected bool isReloading = false;

    private float shootTimer = 0;

    public virtual void Start()
    {
        curAmmo = maxAmmo;
    }

    public virtual void Update()
    {
        //Debug.DrawRay(muzzle.position, muzzle.forward * range, Color.red);
        shootTimer += Time.deltaTime;
        if (shootTimer >= rateOfFire)
        {
            canShoot = true;
        }

        if (curAmmo == 0)
        {
            canShoot = false;
        }

        UI.instance.ammoText.text = ""+curAmmo;
    }

    public virtual void PrimaryFire() { }
    public virtual void SecondaryFire() { }

    public virtual void Reload()
    {
        StartCoroutine(ReloadSequence(reloadDelay));
    }

    IEnumerator ReloadSequence(float delay)
    {
        isReloading = true;

        yield return new WaitForSeconds(delay);
        curAmmo = maxAmmo;

        isReloading = false;
        canShoot = true;
    }
}
