using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexController : MonoBehaviour {

	public float maxVelocity;
	public float jumpTakeOffVelocity;
	public float minMoveDistance;

	public float gravMod = 1f;

	private bool grounded;
	private Vector2 targetVelocity;
	private Vector2 velocity;
	private Vector2 groundNormal;


	private Rigidbody2D rb2d;
	private Animator animator;
	private SpriteRenderer sr;

	void onEnable() {
		
	}

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		//velocity += gravMod * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

		Vector2 move = moveAlongGround * deltaPosition.x;

		Movement (move, false);

		move = Vector2.up * deltaPosition.y;

		Movement (move, true);
	}

	void Movement (Vector2 move, bool yMovement) {
		float distance = move.magnitude;

		if (distance > minMoveDistance) {
			
		}

		rb2d.position = rb2d.position + move.normalized * distance;
	}

	private void ComputeVelocity() {
		
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffVelocity;
		} else if (Input.GetButtonUp ("Jump")) {
			if (velocity.y > 0) {
				velocity.y = velocity.y * 0.5f;
			}
		}

		bool flipSprite = (sr.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (flipSprite) {
			sr.flipX = !sr.flipX;
		}

		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxVelocity);

		targetVelocity = move * maxVelocity;
	}

}
