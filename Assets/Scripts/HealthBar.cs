using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthBar;

    // Use this for initialization
    void Start()
    {
        healthBar = GetComponent<Slider>();

        healthBar.value = curHealth.value;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
