using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoneThrower : Weapon
{
    //List<Quaternion> pellets;
    [Header("Objects")]
    public GameObject wall;
    public GameObject projectile;
    public Transform muzzle;
    public LineRenderer line;
    public GameObject particleHit;
    [Header("Spread")]
    public int pelletsCount = 8;
    public float pelletSpeed = 80;
    public float pelletSpread = 5f;
    [Header("Wall")]
    public float spawnWallRange = 20f;
    //public Transform rayOrigin;
    [Header("Camera")]
    public Camera playerCamera;
    private float range = 100f;
    //public Transform target;
    public override void Start()
    {
        base.Start(); // Set the current ammo!
        playerCamera = GetComponentInParent<Camera>();
        line = GetComponentInChildren<LineRenderer>();
    }
    
    List<Ray> rays = new List<Ray>();
    List<Vector3> hits = new List<Vector3>();
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var ray in rays)
        {
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
        }

        Gizmos.color = Color.blue;
        foreach (var hit in hits)
        {
            Gizmos.DrawSphere(hit, .1f);
        }
    }

    public override void PrimaryFire()
    {
        if (canShoot)
        {
            #region Old Crap
            Ray shotgunRay = new Ray(muzzle.position, muzzle.forward);
            #endregion

            rays = new List<Ray>();
            hits = new List<Vector3>();
            bool hitSomething = false;

            for (int i = 0; i < pelletsCount; i++)
            {
                RaycastHit hit;

                //Vector3 direction = (muzzle.forward + new Vector3(pitch, yaw, roll)) * Mathf.Rad2Deg;
                #region Manny's
                float pitch = Random.Range(-pelletSpread, pelletSpread);
                float yaw = Random.Range(-pelletSpread, pelletSpread);
                float roll = Random.Range(-pelletSpread, pelletSpread);
                Quaternion spreadRotation = muzzle.rotation *
                    Quaternion.AngleAxis(pitch, muzzle.right) *
                    Quaternion.AngleAxis(yaw, muzzle.up) *
                    Quaternion.AngleAxis(roll, muzzle.forward);
                #endregion

                Quaternion randomRotation = Random.rotation;
                spreadRotation = Quaternion.RotateTowards(spreadRotation, randomRotation, Random.Range(0.0f, pelletSpread));
                print(spreadRotation);

                Physics.Raycast(muzzle.position, spreadRotation * Vector3.forward, out hit, range);
                GameObject clone = Instantiate(projectile, muzzle.position, spreadRotation);
                clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * pelletSpeed;

                Ray bulletRay = new Ray(muzzle.position, spreadRotation * Vector3.forward);
                rays.Add(bulletRay);


                if (Physics.Raycast(bulletRay.origin, bulletRay.direction, out hit, range))
                {
                    //PlayerHealth health = hit.collider.GetComponent<PlayerHealth>();
                    //health.TakeDamage(damage);
                    hit.collider.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                    Instantiate(particleHit, hit.point, spreadRotation);
                    hitSomething = true;
                    hits.Add(hit.point);
                }



            }
            curAmmo--;

            canShoot = false;

        }
    }

    public override void SecondaryFire()
    {
        if (canShoot && curAmmo >= maxAmmo / 2)
        {
            Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, spawnWallRange))
            {

                Transform target = GameObject.Find("Player").GetComponent<Transform>();


                GameObject spawnWall = Instantiate(wall, hit.point + new Vector3(0, 0, 0), Quaternion.LookRotation(-target.transform.right)) as GameObject;
                curAmmo -= 12;

            }
        }


    }
  
    //IEnumerator ShotLine(Ray shotgunRay, float lineDelay)
    //{
    //    //Run logic before
    //    line.enabled = true;
    //    line.SetPosition(0, shotgunRay.origin);
    //    line.SetPosition(1, shotgunRay.origin + shotgunRay.direction * range);
    //    yield return new WaitForSeconds(lineDelay);
    //    //Run logic after
    //    line.enabled = false;
    //}
}

