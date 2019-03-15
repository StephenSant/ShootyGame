using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    public Rigidbody rigid;

    public bool isGrounded;

    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
        GroundCheck();
    }

    public void Move()
    {
        rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime));
    }
    public void Jump()
    {
        rigid.AddForce(transform.up * jumpHeight,ForceMode.Impulse);
    }
    void GroundCheck()
    {
        RaycastHit hit;
        float distance = 1.1f;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
