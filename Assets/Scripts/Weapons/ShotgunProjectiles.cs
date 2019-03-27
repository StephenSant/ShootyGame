using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectiles : MonoBehaviour
{
    public int damage = 8;
    public Rigidbody bRigid;
    public int bSpeed = 10;
	// Use this for initialization
	void Start ()
    {
		
	}
	
    public void Fire(Vector3 direction)
    {
        bRigid.AddForce(direction * bSpeed, ForceMode.Impulse);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
