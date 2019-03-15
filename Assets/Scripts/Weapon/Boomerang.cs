using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {

    public float speed;
    public bool hasBounced;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
        rigid.velocity = Vector3.forward*speed*Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
