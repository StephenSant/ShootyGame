using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoneThrower : Weapon
{
    #region Variables
    [Header("Objects")]
    public GameObject wall;
    public GameObject projectile;
    public Transform muzzle;
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
    #endregion

    public override void Start()
    {
        base.Start(); // Set the current ammo!
        //Gets camera
        playerCamera = GetComponentInParent<Camera>();
        //overrides weapon parent fire rate value on start to 1.2f
        rateOfFire = 1.2f;
        
    }
    public override void Update()
    {
        base.Update();
    }
    //Gets a list of rays
    List<Ray> rays = new List<Ray>();
    List<Vector3> hits = new List<Vector3>();
    private void OnDrawGizmos()
    {
        //Draws rays
        Gizmos.color = Color.red;
        foreach (var ray in rays)
        {
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
        }
        //Draws blue sphere on each raycast hit
        Gizmos.color = Color.blue;
        foreach (var hit in hits)
        {
            Gizmos.DrawSphere(hit, .1f);
        }
    }

    public override void PrimaryFire()
    {
        //if the player can shoot and isnt reloading
        if (canShoot == true && isReloading == false)
        {
            
            //Creates a list of rays
            rays = new List<Ray>();
            hits = new List<Vector3>();
            bool hitSomething = false;
            //loops through array of pellets
            for (int i = 0; i < pelletsCount; i++)
            {
                
                RaycastHit hit;

                #region Manny's
                //Shotgun pellet spread
                float pitch = Random.Range(-pelletSpread, pelletSpread);
                float yaw = Random.Range(-pelletSpread, pelletSpread);
                float roll = Random.Range(-pelletSpread, pelletSpread);
                Quaternion spreadRotation = muzzle.rotation *
                    Quaternion.AngleAxis(pitch, muzzle.right) *
                    Quaternion.AngleAxis(yaw, muzzle.up) *
                    Quaternion.AngleAxis(roll, muzzle.forward);
                #endregion
                //random rotation of spread
                Quaternion randomRotation = Random.rotation;
                //rotate towards axis
                spreadRotation = Quaternion.RotateTowards(spreadRotation, randomRotation, Random.Range(0.0f, pelletSpread));
                print(spreadRotation);

                //Shoots out a raycast from the muzzle position
                Physics.Raycast(muzzle.position, spreadRotation * Vector3.forward, out hit, range);
                //Creates a gameobject bullet
                GameObject clone = Instantiate(projectile, muzzle.position, spreadRotation);
                //adds velocity to bullet rigidbody
                clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * pelletSpeed;
                //destroys itself after 1.75 secs
                Destroy(clone, 1.75f);
                //Creates rays from the list of rays, representing each pellet and its spread rotation 
                Ray bulletRay = new Ray(muzzle.position, spreadRotation * Vector3.forward);
                rays.Add(bulletRay);

                //if it hits something
                if (Physics.Raycast(bulletRay.origin, bulletRay.direction, out hit, range))
                {
                    //take damage from health script on hit object
                    hit.collider.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                    //Creates bullet impact particle effect on hit
                    GameObject particleClone = Instantiate(particleHit, hit.point, spreadRotation);
                    //destroy the particle effect
                    Destroy(particleClone, 1.1f);
                    hitSomething = true;
                    hits.Add(hit.point);
                }



            }
            //take 1 bullet away from mag
            curAmmo--;
            //resets shoot timer
            shootTimer = 0;
            //cant shoot
            canShoot = false;

        }
    }

    public override void SecondaryFire()
    {
        //if there is maxammo and also grater than half of the ammo cap in the mag
        if(curAmmo >= maxAmmo / 2)
        {
            //if can shoot and isnt reloadig
            if (canShoot = true && isReloading == false)
            {
                //shoots a ray from the camera to the point on screen
                Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(camRay, out hit, spawnWallRange))
                {
                    //spawns wall and rotates/faces wall to player
                    Transform target = GameObject.Find("Player").GetComponent<Transform>();
                    GameObject spawnWall = Instantiate(wall, hit.point + new Vector3(0, 0, 0), Quaternion.LookRotation(-target.transform.right)) as GameObject;
                    curAmmo -= 12;
                    //takes 12 bullets away from magazine

                }
            }
        }



    }
    public override void Reload()
    {
        //Overrides reload timer from weapon script
        StartCoroutine(ReloadSequence(3.2f));
        
    }

}

