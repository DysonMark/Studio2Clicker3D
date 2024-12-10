using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordAttack : MonoBehaviour
{
    public int damage;

    [Range(0, 1)] public float maxDistance;

    public bool hasHit;

    private string tt;
    
    public EnemyAI _enemyAI;

    public Slider enemySlider;


    // Update is called once per frame
    void Update()
    {
        enemySlider.value = _enemyAI.enemyHP;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, maxDistance))
        {
            Debug.Log("hit: " + hit.transform.name);
            if (hit.transform.root.GetComponent<EnemyAI>() != null && !hasHit)
            {
                hit.transform.root.GetComponent<EnemyAI>().EnemyTakeDamage(damage, _enemyAI._animator);
                _enemyAI.enemyHP = hit.transform.root.GetComponent<EnemyAI>().enemyHP;
                enemySlider.value = hit.transform.root.GetComponent<EnemyAI>().enemyHP;
                hasHit = true;
                Debug.Log(_enemyAI.enemyHP);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.up * maxDistance);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 50,200,20), tt);
    }
}
