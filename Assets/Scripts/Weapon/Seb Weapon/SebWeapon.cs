using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seb
{
    public class SebWeapon : MonoBehaviour
    {        
        public int damage = 34;
        public float speed = 5f;
        public int ammo = 6;
        public float rateOfFire = 5f;
        public GameObject projectile;
        public Transform shootPoint;
        protected int currentAmmo = 0;
        public bool fire;
        public Rigidbody rigid;
        public ParticleSystem particle;


        //public abstract void Attack();

        public void Reload()
        {
            currentAmmo = ammo;
        }

        public void Fire(Vector3 direction)
        {
            GameObject clone = Instantiate(projectile, shootPoint.transform.position, transform.rotation);
            rigid.AddForce(direction * speed, ForceMode.Impulse);
            
        }


    }
}
