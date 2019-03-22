using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Values")]
    public float moveSpeed = 7.5f;
    public float jumpHeight = 20f;

    [Header("Physics")]
    public float gravity = 10f;
    public float groundRayDistance = 1.1f;

    [Header("References")]
    public CharacterController controller;

    Vector3 movement;
    float jumpTime;

    #region Initialisation
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    #endregion

    private void Move(float inputH, float inputV)
    {
        Vector3 input = new Vector3(inputH, 0, inputV);
        input = transform.TransformDirection(input);
        movement.x = input.x * moveSpeed;
        movement.z = input.z * moveSpeed;
    }

    void Movement()
    {
        float inputV = Input.GetAxis("Vertical");
        float inputH = Input.GetAxis("Horizontal");
        Move(inputH, inputV);
        Ray groundRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(groundRay, out hit, groundRayDistance))
        {
            if (Input.GetButton("Jump"))
            {
                movement.y = jumpHeight;
            }
            else
            {
                movement.y = 0;
            }
        }
        else
        {
            if (movement.y < -gravity)
            {
                movement.y = -gravity;
            }
        }
        movement.y -= gravity * Time.deltaTime;
        controller.Move(movement * Time.deltaTime);
    }

    void Shooting()
    {

    }

    void Update()
    {
        Movement();
        Shooting();
    }
}
