using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed = 0;

	public float Timer = 0;

	public float dashMult = 0;
	

	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		ControlPlayer();
	}
	

	void ControlPlayer()
	{
		//Dash timer resetting on update
		if (Timer < 1)
		{
			Timer += Time.deltaTime;
		}
		//Move
		//Initialize axis on button press
		if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
		{
			float moveHorizontal = Input.GetAxisRaw("Horizontal");
			float moveVertical = Input.GetAxisRaw("Vertical");
			//Slow diagnal movement so they don't feel faster/additive
			if (Input.GetButton("Vertical") && Input.GetButton("Horizontal"))
			{
				moveHorizontal = moveHorizontal / 1.5f;
				moveVertical = moveVertical / 1.5f;
			}
			
			//Dash Initialization
			if (Input.GetKeyDown("left shift") && Timer >= .5f)
			{
				dashMult = 5;
				Timer = 0;
			}
			//While dashMult is more than 1 it will slow down. Change the number value *Time.deltaTime to effect how long the dash is
			if (dashMult > 1)
			{
				dashMult -= 10 * Time.deltaTime;
			}
			else
			{
				dashMult = 1;
			}
			

			//Move and Look Direction
			Vector3 movement = new Vector3(moveHorizontal*dashMult, 0.0f, moveVertical*dashMult);
			transform.rotation = Quaternion.LookRotation(movement);


			transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
		}
	}
}
