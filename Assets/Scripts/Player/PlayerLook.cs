﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour//Stephen
{
    PlayerManager manager;
    public float sensitivityX = .75f;
    public float sensitivityY = .75f;
    public float minimumY = -75;
    public float maximumY = 75;

    float rotationY, rotationX;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    private void Start()
    {

        HideCursor(true);
    }

    // Update is called once per frame
    void Update ()
    {
        //looks up and down
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);//CLAMP!
        manager.firstPersonCamera.transform.localRotation = Quaternion.Euler(-rotationY,0, 0);

        //looks left and right
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        transform.rotation = Quaternion.Euler(0,rotationX,0);
    }

    public void HideCursor(bool hide)
    {
        if (hide)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    } 
}
