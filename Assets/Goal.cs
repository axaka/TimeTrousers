using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Goal : MonoBehaviour
{
	public bool HasPlayer
	{
		get; private set;
	}
	
	void Start()
	{

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//if (col.GetComponent<Player>())
		//{
		//	HasPlayer = true;
		//}
	}
	
	void Update()
	{

	}
}