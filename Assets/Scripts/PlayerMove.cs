using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	[SerializeField]
	float jumpForce = 10f;

	[SerializeField]
	float moveForce = 10f;

	Rigidbody2D rb;

	bool grounded = false;
	[SerializeField]
	private float jumpCheckDistance;

	[SerializeField]
	float rayWidth;

	[SerializeField]
	LayerMask groundCheck;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit2D[] rHit = new RaycastHit2D[3];
		for (int i = -1; i<2; i++)
		{
			rHit[i+1] = Physics2D.Raycast(transform.position + (Vector3.right * rayWidth * i), Vector3.down * jumpCheckDistance, jumpCheckDistance, groundCheck);
			Debug.DrawRay(transform.position + (Vector3.right * rayWidth * i), Vector3.down * jumpCheckDistance, Color.red);
			//Debug.DrawRay(transform.position, Vector3.down)

		}

		if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y < 0.1f)
		{
			bool hit = false;
			for (int i = 0; i<3; i++)
			{
				if (rHit[i])
					hit = true;

			}

			if (hit)
			{
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			grounded = false;
			}

		}
		
	}

	void FixedUpdate()
	{
		Vector2 move = Vector2.right * Input.GetAxisRaw("Horizontal") * moveForce * Time.deltaTime;
		transform.Translate(move);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		grounded = true;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		//grounded = false;
	}
}
