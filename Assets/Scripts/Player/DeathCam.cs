using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCam : MonoBehaviour
{
    public float pullBackSpeed;
    public float pullBackTime;
    float pullBackTimer;
    // Use this for initialization
    void Start()
    {
        pullBackTimer = pullBackTime;
    }

    // Update is called once per frame
    void Update()
    {
        pullBackTimer -= Time.deltaTime;
        if (pullBackTimer > 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime * pullBackSpeed);
        }
    }
}
