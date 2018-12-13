using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour {
	public float movementSpeed = 0;

	public float Timer = 0;

	public float dashMult = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
     
		ControllPlayer();
	}


	void ControllPlayer()
	{
		//Dash timer resetting on update
		if (Timer < 1)
		{
			Timer += Time.deltaTime;
		}
		//Move
		//Initialize axis on button press
		if (Input.GetButton("Vertical2") || Input.GetButton("Horizontal2"))
		{
			float moveHorizontal = Input.GetAxisRaw("Horizontal2");
			float moveVertical = Input.GetAxisRaw("Vertical2");
			//Slow diagnal movement so they don't feel faster/additive
			if (Input.GetButton("Vertical2") && Input.GetButton("Horizontal2"))
			{
				moveHorizontal = moveHorizontal / 1.5f;
				moveVertical = moveVertical / 1.5f;
			}
			
			//Dash Initialization
			if (Input.GetKeyDown("right shift") && Timer >= .5f)
			{
				dashMult = 4.5f;
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
