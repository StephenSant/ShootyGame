﻿using System.Collections;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    public float speed = 2f;
    CharacterController player;

    float moveFB;
    float moveLR;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveFB = Input.GetAxis ("Vertical") * speed;
        moveLR = Input.GetAxis ("Horiozontal") * speed;

        Vector3 movement = new Vector3 (moveLR, moveFB, 0);
        movement = transform.rotation * movement;
	}
}
