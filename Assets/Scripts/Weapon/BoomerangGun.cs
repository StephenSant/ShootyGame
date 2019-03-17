using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangGun : MonoBehaviour {

    public Transform firePoint;
    public GameObject boomerang;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(boomerang, firePoint.position, firePoint.rotation);
        }
	}
}
