using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{
	public float speed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
		{
			transform.position+=transform.TransformDirection(Vector3.forward)*speed;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position-=transform.TransformDirection(Vector3.forward)*speed;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.up*10f);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.up*-10f);
		}
	}
}
