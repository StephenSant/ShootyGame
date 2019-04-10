using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : Weapon
{
    //public float fireRate = .25f;
    //public float weaponRange = 50f;
    public float hitForce = 100f;
    //public Transform gunEnd;

    //public int maxAmmo = 10;
    //private int currentAmmo;
    //public float reloadTime = 1f;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserline;
    private float nextFire;
    public float charge;

    // Use this for initialization
    override public void Start()
    {
        curAmmo = maxAmmo;

        laserline = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    public override void PrimaryFire()
    {
        if (canShoot)
        {
            //WIDTH?? set min
            laserline.startWidth = 0.01f;
            nextFire = Time.time + rateOfFire;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserline.SetPosition(0, firePoint.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
            {
                laserline.SetPosition(1, hit.point);
                Health health = hit.collider.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserline.SetPosition(1, rayOrigin + (fpsCam.transform.forward * range));
            }
            curAmmo--;
            shootTimer = 0;
        }
    }

    public override void SecondaryFire()
    {
        if (canShoot)
        {
            //Charge
            if (charge < 5)
            {
                charge += Time.deltaTime;

            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (charge > 5)
        {
            float ouch = damage + charge;

            charge = Mathf.Clamp01(charge / 5);
            //WIDTH?? set new size

            laserline.startWidth = charge;

            Debug.Log("" + ouch);
            nextFire = Time.time + rateOfFire;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserline.SetPosition(0, firePoint.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
            {
                laserline.SetPosition(1, hit.point);
                Health health = hit.collider.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage((int)ouch);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserline.SetPosition(1, rayOrigin + (fpsCam.transform.forward * range));
            }
            curAmmo -= (int)(damage + charge);
            charge = 0; shootTimer = 0;
        }

    }

    private IEnumerator ShotEffect()
    {
        laserline.enabled = true;
        yield return shotDuration;
        laserline.enabled = false;
    }

}
