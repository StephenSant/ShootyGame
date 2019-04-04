using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;
    public bool isDead;
    [Header("UI")]
    public bool showUI = true;
    public GameObject healthBarPrefab;
    public Transform healthBarParent;
    public Vector3 healthBarOffset = new Vector3(0, 2f, 0);

    private Slider healthBar;


    void SpawnUI()
    {
        GameObject clone = Instantiate(healthBarPrefab, healthBarParent);
        healthBar = clone.GetComponent<Slider>();

        clone.SetActive(showUI);
    }

    // Use this for initialization
    void Start()
    {
        isDead = false;
        curHealth = maxHealth;
        SpawnUI();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
    }
    public void TakeDamage(int damage)
    {
        //current health is minused by damage
        curHealth -= damage;

        // Update slider - Percentage (curr / max)
        healthBar.value = (float)curHealth / (float)maxHealth;

        //if curhealth is less then or equal to 0
        if (curHealth <= 0)
        {
            curHealth = 0;
            //you are dead, no big suprise
            Dead();
        }
    }
    public void Dead()
    {
        //the player is dead
        isDead = true;
        //Destroy player
        Destroy(gameObject);

        // Remove health bar
        Destroy(healthBar.gameObject);
    }
}
