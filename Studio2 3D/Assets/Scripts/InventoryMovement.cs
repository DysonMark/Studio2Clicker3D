using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMovement : MonoBehaviour
{
    public float angle = 10;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, angle * Time.deltaTime); 
    }
}
