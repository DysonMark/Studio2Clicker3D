using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem : MonoBehaviour
{
    
    #region Variables
    private Animator animator;
    
    public CharacterController check;
    CustomActions input;

    [SerializeField] private bool cooldown = false;

    [SerializeField] private float cooldownTime = 2f;

    private bool attacking = false;
    #endregion
    void Awake()
    {
        input = new CustomActions();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AssignAttack();
    }
    
    void AssignAttack()
    {
        input.Main.Attack.performed += ctx => StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (cooldown == false)
        {
            Debug.Log("Player attacking");
            animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1); 
            animator.SetTrigger("Attack");
            cooldown = true;    
            yield return new WaitForSeconds(cooldownTime);
        }
        cooldown = false;
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
}
