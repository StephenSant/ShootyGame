using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectiles : MonoBehaviour
{
    public int damage = 8;
    public Rigidbody bRigid;
    public int bSpeed = 10;
    // Use this for initialization


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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth pHealth = other.gameObject.GetComponent<PlayerHealth>();
            pHealth.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }

}
