using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectiles : MonoBehaviour
{

    public Rigidbody bRigid;

    #region Destroy on Collision
    //If it hits any collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            //if there is a collider
            if(collision.collider == true)
            {
                //Destroy Gameobject
                Destroy(gameObject);
            }

        }
    }

    //If it hits any collider trigger with player tag or stonethrow wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy Gameobject
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "StoneThrowerWall")
        {
            //Destroy Gameobject
            Destroy(gameObject);
        }
    }
    #endregion
}
