using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public float health;
    public float maxHealth = 100f;
    public Slider healthSlider;

    void Start()
    {
        health = maxHealth;
    }
    
    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            SceneManager.LoadScene("_Dyson_GAD213/Scenes/Area1");
        }
    }
}
