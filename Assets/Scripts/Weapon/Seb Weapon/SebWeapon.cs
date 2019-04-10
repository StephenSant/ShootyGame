using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SebWeapon : Weapon
{
    public int newDamage = 34;
    public float speed = 5f;
    public int newMaxAmmo = 30;
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
            }

            //takes 1 bullet away from the current ammo pool
            curAmmo--;
            //resets the fire rate timer to 0
            shootTimer = 0;
            StartCoroutine(ShowLine(start, end, lineDelay));
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

