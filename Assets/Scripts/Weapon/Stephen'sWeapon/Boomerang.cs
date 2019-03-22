using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public bool isReturning = false;
    public float speed = 500;
    public float returnSpeed = -500;
    public bool fall = false;
    public Transform returnPoint;

    Rigidbody rigid;
    Vector3 dir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("It dont think it be like it is but it do");
        }
        if (isReturning)
        {
            rigid.useGravity = true;
            rigid.freezeRotation = false;
            fall = true;
        }
        else
        {
            isReturning = true;
        }
    }

    void Update()
    {
        if (!fall)
        {
            if (isReturning)
            {
                dir = (transform.position - returnPoint.position).normalized;
                rigid.velocity = dir * returnSpeed * Time.deltaTime;
            }
            else
            {
                dir = transform.forward;
                rigid.velocity = dir * speed * Time.deltaTime;
            }
        }
    }
}
