using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera fPCamera;

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    [SerializeField]
    float rotationY, rotationX;

    private void Awake()
    {
        fPCamera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        HideCursor(true);
    }

    // Update is called once per frame
    void Update ()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);//CLAMP!
        fPCamera.transform.localRotation = Quaternion.EulerAngles(-rotationY,0, 0);

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
