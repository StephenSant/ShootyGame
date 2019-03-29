using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoneThrower : MonoBehaviour
{
    //List<Quaternion> pellets;
    [Header("Objects")]
    public GameObject wall;
    public GameObject projectile;
    public Transform muzzle;
    public LineRenderer line;
    [Header("Ammo")]
    public int curAmmo;
    public int maxAmmo = 20;
    private bool canShoot;
    private float shootTimer = 0;
    public bool isReloading;
    [Header("Spread")]
    public int pelletsCount = 8;
    public float pelletSpeed = 10;
    public float pelletSpread = 5f;
    public float rateOfFire = 2.6f;
    [Header("Wall")]
    public float spawnWallRange = 20f;
    //public Transform rayOrigin;
    [Header("Camera")]
    public Camera playerCamera;
    private float range = 100f;
    private float lineDelay = 1f;
    //public Transform target;
    public void Start()
    {
        playerCamera = GetComponentInParent<Camera>();
        curAmmo = maxAmmo;
        canShoot = true;
        line = GetComponentInChildren<LineRenderer>();
    }
    public void Update()
    {

    }
    public void FixedUpdate()
    {

        RaycastHit hit;
        //Debug.DrawRay(muzzle.position, muzzle.forward * range, Color.red);
        shootTimer += Time.deltaTime;

        if (shootTimer >= rateOfFire)
        {
            canShoot = true;
        }

        //if (timeToShoot >= rateOfFire)
        //{
        //    //canShoot = true;
        //}
        if (curAmmo == 0)
        {
            canShoot = false;
        }
        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
            if (Input.GetMouseButtonDown(1))
            {
                SpawnWall();
            }
        }
        if (curAmmo == 0 || curAmmo < maxAmmo)
        {
            if (Input.GetKey(KeyCode.R))
            {

                canShoot = false;
                StartCoroutine(ReloadSequence());


            }
        }
        //if (Input.GetKey(KeyCode.R) && canShoot == false)
        //{
        //    if(curAmmo == 0 || curAmmo < maxAmmo)
        //    {
        //        StartCoroutine(ReloadSequence());
        //    }

        //}

    }
    public void Attack()
    {

        if (canShoot)
        {

            Ray shotgunRay = new Ray(muzzle.position, muzzle.forward);
            RaycastHit hit;
            //Debug.DrawRay(muzzle.position, muzzle.forward * range, Color.red);
            Physics.Raycast(shotgunRay, out hit, range);

            for (int i = 0; i < pelletsCount; i++)
            {
                //Vector3 direction = (muzzle.forward + new Vector3(pitch, yaw, roll)) * Mathf.Rad2Deg;

                float pitch = Random.Range(-pelletSpread, pelletSpread);
                float yaw = Random.Range(-pelletSpread, pelletSpread);
                float roll = Random.Range(-pelletSpread, pelletSpread);
                Quaternion rotation = muzzle.rotation *
                    Quaternion.AngleAxis(pitch, muzzle.right) *
                    Quaternion.AngleAxis(yaw, muzzle.up) *
                    Quaternion.AngleAxis(roll, muzzle.forward);

                print(rotation);

                //GameObject clone = Instantiate(projectile, muzzle.position, rotation);
                //clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * pelletSpeed;

                Transform originalMuzzle = muzzle;
                muzzle.rotation *= rotation;

                Ray spreadRay = new Ray(muzzle.position, muzzle.forward);

                if (Physics.Raycast(spreadRay, out hit, range))
                {
                    print(hit.collider.name);
                    Debug.DrawLine(muzzle.position, hit.point);
                }

                muzzle = originalMuzzle;

                //Destroy(clone, 5);

            }
            curAmmo--;


            StartCoroutine(ShotLine(shotgunRay, lineDelay));
            shootTimer = 0;
            canShoot = false;

            #region Steven's stuff
            //if (Physics.Raycast(shotgunRay, out hit, range))
            //{

            //for (int i = 0; i < pelletsCount; i++)
            //{
            //Vector3 direction = (muzzle.forward + new Vector3(pitch, yaw, roll)) * Mathf.Rad2Deg;

            //float pitch = Random.Range(-pelletSpread, pelletSpread);
            //float yaw = Random.Range(-pelletSpread, pelletSpread);
            //float roll = Random.Range(-pelletSpread, pelletSpread);
            //Vector3 rotation = new Vector3(Random.Range(-pelletSpread, pelletSpread), Random.Range(-pelletSpread, pelletSpread), range);
            ////Quaternion rotation = muzzle.rotation *
            ////    Quaternion.AngleAxis(pitch, muzzle.right) *
            ////    Quaternion.AngleAxis(yaw, muzzle.up) *
            ////    Quaternion.AngleAxis(roll, muzzle.forward);
            //Debug.DrawRay(muzzle.position, muzzle.forward + rotation, Color.red);
            //Ray shotgunRay = new Ray(muzzle.position, muzzle.forward + rotation);
            //RaycastHit hit;
            //Physics.Raycast(shotgunRay, out hit, range);
            //StartCoroutine(ShotLine(muzzle.position, muzzle.position + rotation/*, lineDelay*/));
            //GameObject clone = Instantiate(projectile, muzzle.position, rotation);
            //clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * pelletSpeed;
            //Destroy(clone, 5);

            //}
            //curAmmo--;



            //shootTimer = 0;
            //canShoot = false;
            // }
            #endregion


        }



    }
    public void SpawnWall()
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
    IEnumerator ReloadSequence()
    {
        isReloading = true;

        yield return new WaitForSeconds(3.1f);
        curAmmo = maxAmmo;

        isReloading = false;
        canShoot = true;
    }
    IEnumerator ShotLine(Ray shotgunRay, float lineDelay)
    {
        //Run logic before
        line.enabled = true;
        line.SetPosition(0, shotgunRay.origin);
        line.SetPosition(1, shotgunRay.origin + shotgunRay.direction * range);
        yield return new WaitForSeconds(lineDelay);
        //Run logic after
        line.enabled = false;
    }
}

