using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    public Rigidbody rigid;

    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //moves the player 
        rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime));
        //jump
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(transform.up * jumpHeight);
        }
    }
}
