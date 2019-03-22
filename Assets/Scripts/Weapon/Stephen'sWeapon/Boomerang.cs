using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {

    public float speed = 500;
    public bool hasBounced = false;
    public Transform player;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBounced)
        {
            rigid.useGravity = true;
            rigid.freezeRotation = false;
            speed = 0;
            GetComponent<Boomerang>().enabled = false;
        }
        else
        {
            hasBounced = true;
            speed = -speed;
        }
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        Vector3 dir;
        if (hasBounced)
        {
            dir = (transform.position - player.position).normalized;
        }
        else
        {
            dir = transform.forward;
        }
		rigid.velocity = dir*speed*Time.deltaTime;
	}
}
