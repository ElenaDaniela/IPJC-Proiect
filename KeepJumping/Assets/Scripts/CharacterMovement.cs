using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    public PhysicsMaterial2D bounceMat, normalMat; // materialele
    private CircleCollider2D rb;
    
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float runSpeed = 40f;
    private float horizontalMove = 0f;
    private float jumpForce = 0f;
    private System.DateTime jumpTime = System.DateTime.Now;
    private bool chargingJump = false;
    

    private void Start()
    {
        rb = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (jumpForce > 0)
        {
            rb.sharedMaterial = bounceMat;
            print(rb.sharedMaterial);
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpTime = System.DateTime.Now;
            chargingJump = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpForce = (float)(System.DateTime.Now - jumpTime).TotalSeconds;
            if (jumpForce > 1f)
                jumpForce = 1f;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jumpForce);
        jumpForce = 0f;
        chargingJump = false;
    }
}
