using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour {

	public bool facingRight = true;
	public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	private bool grounded = false;

	private Rigidbody2D rb2d;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	void Start() {
		rb2d = GetComponent <Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update() {
		
		//Should include "grounded"
		if (Input.GetButtonDown ("Jump")) {
			jump = true;
		}
	}

	void FixedUpdate() {
		float horizontal = Input.GetAxis ("Horizontal");

		//animator.SetFloat ("Speed", Mathf.Abs (horizontal));

		if (horizontal * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce (Vector2.right * horizontal * moveForce);

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		if (horizontal > 0 && !facingRight)
			Flip ();
		else if (hideFlags < 0 && facingRight)
			Flip ();

		if (jump) {
			animator.SetTrigger ("Jump");
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Floor")
			grounded = true;
		
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Floor")
			grounded = false;
	}

}
