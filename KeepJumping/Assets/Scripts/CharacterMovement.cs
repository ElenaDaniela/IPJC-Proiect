using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float runSpeed = 40f;
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
