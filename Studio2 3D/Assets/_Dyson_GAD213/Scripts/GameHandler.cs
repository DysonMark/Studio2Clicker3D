using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public HealthSystem damage;
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        Debug.Log("Health: " + healthSystem.GetHealth());
        Debug.Log("Health: " + healthSystem.GetHealth());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
