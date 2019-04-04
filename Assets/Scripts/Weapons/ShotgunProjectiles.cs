using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectiles : MonoBehaviour
{
    public int damage = 8;
    public Rigidbody bRigid;
    public int bSpeed = 10;
    // Use this for initialization
    public float range = 10f;
    public Transform muzzle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            if(collision.collider == true)
            {
                Destroy(gameObject);
            }

        }
    }

    public void HitEnemy()
    {
        
        //RaycastHit hit;
        //if(Physics.Raycast())
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "StoneThrowerWall")
        {
            Destroy(gameObject);
        }
    }

}
