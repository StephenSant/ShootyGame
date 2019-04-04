using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int damage;
    [Header("Shooting")]
    public int curAmmo;
    public int maxAmmo;
    public float rateOfFire = 100f;
    public float reloadDelay = .5f;
    public bool canShoot = true;

    protected bool isReloading = false;

    public float shootTimer = 0;

    public virtual void Start()
    {
        curAmmo = maxAmmo;
    }

    public virtual void Update()
    {
        shootTimer += Time.deltaTime;
        Debug.Log(shootTimer+" "+rateOfFire);
        if (shootTimer >= rateOfFire)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        if (curAmmo <= 0)
        {
            curAmmo = 0;
            canShoot = false;
        }

        UI.instance.ammoText.text = ""+curAmmo+"/"+maxAmmo;
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
