using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	private Ray ray = new Ray();
	private RaycastHit rayHit = new RaycastHit();
	
	public GameObject CurrentlyHeldObject;

	public KeyCode takeObject;
	

	//Depending on the system, maybe it should be a string array or just a bunch of tags
	public String[] acceptableTag;
	
	void Start ()
	{
		
	}
	
	void Update () {

		
		if (Input.GetKeyDown(takeObject))
		{
			if (CurrentlyHeldObject == null)
			{
				pickupObject();
			}
			else
			{
				dropObject();
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

	public bool dropObject()
	{
		//1. set transform of CurrentlyHeldObject to be child of nothing
		//2. Set other object as not kinematic and apply gravity
		//3. set CurrentlyHeldObject as null

		if (CurrentlyHeldObject != null)
		{
			CurrentlyHeldObject.transform.SetParent(null);
			CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = true;
			CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = false;
			
			CurrentlyHeldObject = null;
			
			return true;
		}
		return false;
	}

	public bool pickupObject()
	{
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.magenta);

		if (CurrentlyHeldObject == null)
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 1f))
			{
				if (rayHit.transform.GetComponent<MeshRenderer>().tag == "Tomato")
				{
					Debug.Log("Hit " + rayHit);
					rayHit.transform.SetParent(this.transform);
					CurrentlyHeldObject = rayHit.transform.gameObject;
					return true;
				}
				else
				{
					Debug.Log("Can't pick up " + rayHit.transform.name);
					return false;
				}
			}
		}

		return false;
	}
	
	
	//For now: if you run into ab object, you pick it up
	//Future: if raycast hit an object, drop current object to pick it up

	private void OnCollisionEnter(Collision other)
	{
	}
}
