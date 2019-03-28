using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoneThrower : MonoBehaviour
{
    List<Quaternion> pellets;
    [Header("Objects")]
    public GameObject wall;
    public GameObject projectile;
    public Transform muzzle;
    [Header("Ammo")]
    public int curAmmo;
    public int maxAmmo = 20;
    private bool canShoot;
    private float timeToShoot = 0;
    public bool isReloading;
    [Header("Spread")]
    public int pelletsCount = 8;
    public float pelletSpeed = 10;
    public float pelletSpread = 5f;
    public float rateOfFire = 1f;
    [Header("Wall")]
    public float spawnWallRange = 20f;
    //public Transform rayOrigin;
    [Header("Camera")]
    public Camera playerCamera;
    //public Transform target;
    public void Start()
    {
        playerCamera = GetComponentInParent<Camera>();
        curAmmo = maxAmmo;
        canShoot = true;
    }
    public void FixedUpdate()
    {
        timeToShoot += Time.deltaTime;
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
                GameObject clone = Instantiate(projectile, muzzle.position, rotation);
                clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * pelletSpeed;
                Destroy(clone, 5);
            }
            curAmmo--;

            
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

}

