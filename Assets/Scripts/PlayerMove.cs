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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * moveForce * Time.deltaTime, ForceMode2D.Impulse);

		if (grounded && Input.GetAxisRaw("Vertical") != 0)
		{
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			grounded = false;
		}
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		grounded = true;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		grounded = false;
	}
}
