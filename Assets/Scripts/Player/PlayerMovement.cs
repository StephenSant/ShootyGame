using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    public CharacterController controller;

    public bool isGrounded;

    Vector3 moveDirection;
    float jumpTime;

    // Use this for initialization
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
        controller.Move(moveDirection);
        GroundCheck();
    }
    public void Jump()
    {
        moveDirection.y++;
        
        jumpTime++;
        if (jumpTime >= jumpHeight)
        {

        }
    }
    public void Move()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * moveSpeed * Time.deltaTime;
       
    }

    void GroundCheck()
    {
        RaycastHit hit;
        float distance = 1.5f;

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
