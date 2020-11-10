using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementDana : MonoBehaviour
{
    public PhysicsMaterial2D bounceMat, normalMat; // materialele
    public float walkSpeed;
    public bool isGrounded;
    public LayerMask groundMask;
    public float jumpValue;
    public bool canJump;
    
    private float moveInput;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cc = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (!isGrounded)
        {
            cc.sharedMaterial = bounceMat;
        }
        else
        {
            cc.sharedMaterial = normalMat;
        }
        
        
        
        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }
        
        isGrounded = Physics2D.OverlapBox(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
            new Vector2(0.5f, 0.5f), 0f, groundMask);

        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.1f;
            if (jumpValue >= 10f)
                jumpValue = 10f;
        }

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (jumpValue > 0f && !isGrounded)
        {
            canJump = false;
            jumpValue = 0;
        }
        
        if (Input.GetKeyUp("space"))
        {
            if (isGrounded)
            {
                
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }
    }
}
