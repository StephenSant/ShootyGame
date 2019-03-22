using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class PlayerLook : MonoBehaviour//Stephen
{
    public Camera firstPersonCamera;

    public float sensitivityX = 0.025F;
    public float sensitivityY = 0.025F;
    public float minimumY = -1.5F;
    public float maximumY = 1.5F;

    float rotationY, rotationX;

    private void Awake()
    {
        firstPersonCamera = GetComponentInChildren<Camera>();
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
        firstPersonCamera.transform.localRotation = Quaternion.EulerAngles(-rotationY,0, 0);

        //looks left and right
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        transform.rotation = Quaternion.EulerAngles(0,rotationX,0);
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
