using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public PhysicsMaterial2D bounceMat, normalMat; // materialele
	private CircleCollider2D m_CircleColider2D;
	
	[SerializeField] private float m_JumpForce = 650f;                          // Amount of force added when the player jumps
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping
	[SerializeField] private bool m_HorizontalControl = false;                  // Whether or not a player can move horizontally
	[Range(0f, 1f)] [SerializeField] private float m_HorizontalModifier = 0.75f;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private BoxCollider2D m_GroundCheck;                       // A position marking where to check if the player is grounded

	private bool m_Grounded;            // Whether or not the player is grounded
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	private void Awake()
	{
		
		m_CircleColider2D = gameObject.GetComponent<CircleCollider2D>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		m_CircleColider2D.sharedMaterial = bounceMat;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapBoxAll(m_GroundCheck.bounds.center, m_GroundCheck.size, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				//devine rough
				m_CircleColider2D.sharedMaterial = normalMat;

				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, float jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			if (m_HorizontalControl)
			{
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}

            if (move > 0 && !m_FacingRight)
				Flip();
			else if (move < 0 && m_FacingRight)
				Flip();
		}
		// If the player should jump...
		if (m_Grounded && jump != 0f)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			// devine bouncy
			m_CircleColider2D.sharedMaterial = bounceMat;
			
			m_Rigidbody2D.AddForce(new Vector2( move * m_JumpForce * jump * m_HorizontalModifier , m_JumpForce * jump));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}