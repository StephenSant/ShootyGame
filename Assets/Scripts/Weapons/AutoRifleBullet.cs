using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifleBullet : MonoBehaviour
{
    public int damage = 5;
    public Rigidbody rb;
    public float bulletSpeed = 15f;
	// Use this for initialization
	void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        DestroyBullet();
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth pHealth = collision.gameObject.GetComponent<PlayerHealth>();
            pHealth.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }

    }
    private void DestroyBullet()
    {
        Destroy(gameObject, 7);
    }
}
