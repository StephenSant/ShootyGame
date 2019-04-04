using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SebWeapon : MonoBehaviour
{
    public int damage = 34;
    public float speed = 5f;
    public int ammo = 30;
    public float maxRateOfFireTime = 1f;
    public float range = 10f;
    public Transform shootPoint;
    protected int currentAmmo;
    private float timerToFire;
    private bool ifCanShoot;
    private LineRenderer lineRenderer;
    private Vector3 hitPoint;

    private void Start()
    {

        currentAmmo = ammo;
    }
    void LineRenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void Reload()
    {
        //current ammo in magazine is now equal to the amount in magazine cap
        currentAmmo = ammo;
    }

    public void Fire()
    {
        //creates a Ray, that starts at the position of the muzzle(A.K.A shootpoint), and directs it on the BLUE axis where the muzzle is facing in the world
        Ray shotRay = new Ray(shootPoint.position, shootPoint.forward);
        //Creates a "hit", which retrieves information where the ray hits
        RaycastHit hit;
        Physics.Raycast(shotRay, out hit, range);
        Debug.Log(hit.point.ToString());
        Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.magenta);
        if (Physics.Raycast(shotRay, out hit, range))   
        {
            hitPoint = hit.point;
            hit.collider.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

        //takes 1 bullet away from the current ammo pool
        currentAmmo--;
        //resets the fire rate timer to 0
        timerToFire = 0;
        //and you cannot fire (clamp on the maximum rate of fire)
        ifCanShoot = false;
        


    }

    void FixedUpdate()
    {
        timerToFire += Time.deltaTime;

        if (timerToFire >= maxRateOfFireTime)
        {
            ifCanShoot = true;
        }
        if (currentAmmo <= 0)
        {
            currentAmmo = 0;
            ifCanShoot = false;
        }
        Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.magenta);


        if (ifCanShoot == true)
        {
            if (Input.GetMouseButton(0))
            {
                Fire();
                GetComponent<LineRenderer>().SetPosition(0, shootPoint.position);
                //if ((hitPoint + shootPoint.position).magnitude >= range)
                //{
                //    GetComponent<LineRenderer>().SetPosition(1, shootPoint.position + (shootPoint.forward * range));
                //}
                //else
                //{
                GetComponent<LineRenderer>().SetPosition(1, shootPoint.position + shootPoint.forward * range);
                //}

                GetComponent<LineRenderer>().enabled = true;

            }
        }

        if (currentAmmo <= 0 || currentAmmo < ammo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }


    }


}

