﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserline;
    private float nextFire;
    public float charge;

	// Use this for initialization
	void Start ()
    {
        if (currentAmmo == -1)
            currentAmmo = maxAmmo;

        laserline = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
            

		if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            //WIDTH?? set min
            laserline.startWidth = 0.01f;
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserline.SetPosition(0, gunEnd.position);

            if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserline.SetPosition(1, hit.point);
                ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserline.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
        if (Input.GetButton("Fire2"))
        {
            //Charge
            if(charge < 3)
            {
                charge += Time.deltaTime;
            }
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            float ouch = gunDamage + charge;

            charge = Mathf.Clamp01(charge/3);
            //WIDTH?? set new size

            laserline.startWidth = charge;

            Debug.Log("" + ouch);
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserline.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserline.SetPosition(1, hit.point);
                ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                if (health != null)
                {
                    health.Damage(ouch);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserline.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
            charge = 0;
        }

    }

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        laserline.enabled = true;
        yield return shotDuration;
        laserline.enabled = false;
    }

    IEnumerator Reload ()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
    }
}
