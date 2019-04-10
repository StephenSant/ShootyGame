using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SebWeapon : Weapon
{
    public int newDamage = 34;
    public float speed = 5f;
    public int newMaxAmmo = 30;
    public int ammo = 30;
    public float maxRateOfFireTime = 1f;
    public float range = 7f;
    public float lineDelay = .05f;
    public Transform shootPoint;
    private LineRenderer lineRenderer;
    private Vector3 hitPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    public override void Start()
    {

        currentAmmo = ammo;
    }

    public void Reload()
    {
        maxAmmo = newMaxAmmo;
        damage = newDamage;
        curAmmo = maxAmmo;
    }

    public override void PrimaryFire()
    {
        if (canShoot)
        {
            //creates a Ray, that starts at the position of the muzzle(A.K.A shootpoint), and directs it on the BLUE axis where the muzzle is facing in the world
            Ray shotRay = new Ray(shootPoint.position, shootPoint.forward);

        //creates a Ray, that starts at the position of the muzzle(A.K.A shootpoint), and directs it on the BLUE axis where the muzzle is facing in the world
        Ray shotRay = new Ray(shootPoint.position, shootPoint.forward);


        Vector3 start = shootPoint.position;
        Vector3 end = shootPoint.position + shootPoint.forward * range;
        
        //Creates a "hit", which retrieves information where the ray hits
        RaycastHit hit;
        Physics.Raycast(shotRay, out hit, range);
        Debug.Log(hit.point.ToString());
        Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.magenta);
        if (Physics.Raycast(shotRay, out hit, range))   
        {
            hitPoint = hit.point;
            end = hitPoint;
            hit.collider.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

        //takes 1 bullet away from the current ammo pool
        currentAmmo--;
        //resets the fire rate timer to 0
        timerToFire = 0;
        //and you cannot fire (clamp on the maximum rate of fire)
        ifCanShoot = false;
        // Enable line (show line as Coroutine)
        StartCoroutine(ShowLine(start, end, lineDelay));
    }

    IEnumerator ShowLine(Vector3 start, Vector3 end, float lineDelay)
    {
        // Update and Show the line
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;

        // Wait a few seconds
        yield return new WaitForSeconds(lineDelay);

        // Hide the line
        lineRenderer.enabled = false;
    }

    void FixedUpdate()
    {
        // Rate of Fire
        timerToFire += Time.deltaTime;

        if (timerToFire >= maxRateOfFireTime)
        {
            ifCanShoot = true;
        }
        // Out of ammo
        if (currentAmmo <= 0)
        {
            // Can't shoot
            currentAmmo = 0;
            ifCanShoot = false;
        }
        Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.magenta);

            Vector3 start = shootPoint.position;
            Vector3 end = shootPoint.position + shootPoint.forward * range;

            //Creates a "hit", which retrieves information where the ray hits
            RaycastHit hit;
            Physics.Raycast(shotRay, out hit, range);
            Debug.DrawRay(shootPoint.position, shootPoint.forward * range, Color.magenta);
            if (Physics.Raycast(shotRay, out hit, range))
            {
                hitPoint = hit.point;
                end = hitPoint;
                if (hit.collider.GetComponent<Health>())
                {
                    hit.collider.GetComponent<Health>().TakeDamage(damage);
                }
                Fire();
                
                //if ((hitPoint + shootPoint.position).magnitude >= range)
                //{
                //    GetComponent<LineRenderer>().SetPosition(1, shootPoint.position + (shootPoint.forward * range));
                //}
                //else
                //{
                //}

            }


            //takes 1 bullet away from the current ammo pool
            curAmmo--;
            //resets the fire rate timer to 0
            shootTimer = 0;
            StartCoroutine(ShowLine(start, end, lineDelay));
        if (currentAmmo <= 0 || currentAmmo < ammo)
        {
            // If R is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Reload weapon
                Reload();
            }

        }
    }

    IEnumerator ShowLine(Vector3 start, Vector3 end, float lineDelay)
    {
        // Update and Show the line
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;

        // Wait a few seconds
        yield return new WaitForSeconds(lineDelay);

        // Hide the line
        lineRenderer.enabled = false;
    }

}

