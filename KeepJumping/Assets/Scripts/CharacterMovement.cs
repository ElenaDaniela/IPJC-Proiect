using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private Animator chargeAnimator;
    private float horizontalMove = 0f;
    private float jumpForce = 0f;
    private System.DateTime jumpTime = System.DateTime.Now;
    private bool chargingJump = false;
    

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpTime = System.DateTime.Now;
            chargingJump = true;
            GetJumpForce();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpForce = GetJumpForce();
        }
        chargeAnimator.SetFloat("JumpCharge", GetJumpForce());
    }

    private void FixedUpdate()
    {
        if (jumpForce == 0f)
            controller.Move(horizontalMove * Time.fixedDeltaTime, 0f);
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jumpForce);
            jumpForce = 0f;
            chargingJump = false;
        }
    }

    private float GetJumpForce()
    {
        float currentJumpForce = 0f;
        if (chargingJump)
        {
            currentJumpForce = (float)(System.DateTime.Now - jumpTime).TotalSeconds;
            if (currentJumpForce > 0.99999f)
                currentJumpForce = 0.99999f;
        }
        return currentJumpForce;
    }
}
