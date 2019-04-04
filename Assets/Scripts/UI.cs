using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance = null;

    [Header("References")]
    public Slider healthBar;
    public Text ammoText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        GameObject.Find("HealthBar").GetComponent<Slider>();
        GameObject.Find("AmmoText").GetComponent<Text>();
    }


}
