using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	private Ray ray = new Ray();
	private RaycastHit rayHit = new RaycastHit();
	
	public GameObject CurrentlyHeldObject;
	

	//Depending on the system, maybe it should be a string array or just a bunch of tags
	public String acceptableTag;
	
	void Start ()
	{
		
	}
	
	void Update () {

		if (Input.GetKey(KeyCode.Space))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.magenta);

			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 1f))
			{
				if (rayHit.transform.GetComponent<MeshRenderer>().tag == "Tomato")
				{
					Debug.Log("Hit " + rayHit);
					rayHit.transform.SetParent(this.transform);
				}
			}
		}

	}

	public void addObject(GameObject other)
	{
		//1. set currently held object as other
		//2. set transform of other object to child of player
		//3. set other object rigidbody as kinematic
		if (CurrentlyHeldObject != null)
		{
			
			CurrentlyHeldObject = other.gameObject;
			other.GetComponent<Transform>().SetParent(this.transform);
		}
		
	}

	public void dropObject()
	{
		//1. set transform of CurrentlyHeldObject to be child of nothing
		//2. Set other object as not kinematic and apply gravity
		//3. set CurrentlyHeldObject as null
		
		
	}
	
	private void OnMouseDown()
	{
		//throw new System.NotImplementedException();
	}
	
	//For now: if you run into ab object, you pick it up
	//Future: if raycast hit an object, drop current object to pick it up

	private void OnCollisionEnter(Collision other)
	{
		if (CurrentlyHeldObject != null)
		{
			//if collider of other thing is "X", add to inventory
			if (other.gameObject.CompareTag(acceptableTag))
			{
				addObject(other.gameObject);
			}
		}
	}
}
