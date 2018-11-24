using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	private Ray ray = new Ray();
	private RaycastHit rayHit = new RaycastHit();
	
	public GameObject CurrentlyHeldObject;
	public int CurrentlyHeldObjectCode;

	// 0 : Tomato
	// 1 : Onion
	// 2 : Pot
	// 3 : Plate
	
	public KeyCode takeObject;

	public bool HoldingThing;
	

	//Depending on the system, maybe it should be a string array or just a bunch of tags
	public String[] acceptableTag;
	
	void Update () {

		
		if (Input.GetKeyDown(takeObject))
		{
			/*if (CurrentlyHeldObject == null)
			{
				Debug.Log("not holding");
				pickupObject();
			}
			else
			{
				Debug.Log("hands r full ");

				dropObjectCheck();
			}*/
			
			if (!HoldingThing)
			{
				Debug.Log("not holding");
				pickupObject();
			}
			else
			{
				Debug.Log("hands r full ");

				dropObjectCheck();
			}
		}

	}

	public void addObject(GameObject other)
	{
		//1. set currently held object as other
		//2. set transform of other object to child of player
		//3. set other object rigidbody as kinematic
		//if (CurrentlyHeldObject != null)
		if(!HoldingThing)
		{
			Debug.Log("HOLD");
			CurrentlyHeldObject = other.gameObject;
			other.GetComponent<Transform>().SetParent(this.transform);
			other.GetComponent<SphereCollider>().enabled = false;
		}
		
	}

	public void dropObjectCheck()
	{
		//1. raycast check to see if there is a pot in front of player
		//2. if yes, check to see if pot is full
		//3. If pot is not full, add vegetable to the array.
		//4. Destroy object
		
		
		//future edits: if you're holding a pot and you wanna hold a different pot, what do???
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.magenta);
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 2.2f))
		{
			Debug.Log("Hit " + rayHit.transform.name);
			if (rayHit.transform.GetComponent<MeshRenderer>().tag == "Pot")

		{
				
				if (CurrentlyHeldObject != null)
				{
					Debug.Log("holding smth");
					//Check tag against array of tags
					string[] tempTag = rayHit.transform.GetComponent<ContainerInventory>().acceptedTag;
					for (int i = 0; i < tempTag.Length; i++)
					{
						//if tag is accepted, check container if can be added
						
						if (CurrentlyHeldObject.tag == tempTag[i])
						{
							Debug.Log("acceptable tag");
							//if accepted, destroy gameobject and reset currentlyheldobject and code
							if (rayHit.transform.GetComponent<ContainerInventory>()
								.addVegetable(CurrentlyHeldObjectCode))
							{
								Debug.Log("pot can take");
								GameObject temp = CurrentlyHeldObject;
								CurrentlyHeldObject = null;
								CurrentlyHeldObjectCode = 500;
								Destroy(temp);
							}
						}
					}
				}
			}
			else
			{
				//Swap object
			}
		}
		
		else
		{
			dropObject();
			//Debug.Log("Dropping object " + rayHit.transform.name);
		}
	}
	
	public bool dropObject()
	{
		//1. set transform of CurrentlyHeldObject to be child of nothing
		//2. Set other object as not kinematic and apply gravity
		//3. set CurrentlyHeldObject as null
		if (CurrentlyHeldObject != null)
		{
			CurrentlyHeldObject.layer = 0;
			CurrentlyHeldObject.transform.SetParent(null);
			CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = true;
			CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = false;
			if (CurrentlyHeldObjectCode > 1)
			{
				CurrentlyHeldObject.GetComponent<MeshCollider>().enabled = true;
				CurrentlyHeldObjectCode = 0;
			}
			else
			{
				CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = true;
			}
			

			//if (rayHit.transform.GetComponent<MeshRenderer>().tag == "Table")
			//{
			//	CurrentlyHeldObject.transform.position = rayHit.transform.position;
			//}
			CurrentlyHeldObject = null;
			HoldingThing = false;
			Debug.Log(CurrentlyHeldObject);
			
			return true;
		}
		return false;
	}

	public bool pickupObject()
	{
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.magenta);

		if (CurrentlyHeldObject == null)
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 2.2f))
			{
				bool isAcceptable = false;
				//fix this later to be more efficient
				for (int i = 0; i < acceptableTag.Length; i++)
				{ 
					if (rayHit.transform.GetComponent<MeshRenderer>().tag == acceptableTag[i])
					{
						isAcceptable = true;
						break;
					}

					if (rayHit.transform.GetComponent<MeshRenderer>().tag == "Pot")
					{
						CurrentlyHeldObjectCode = 500;
					}
				}
				if (isAcceptable)
				{
					Debug.Log("Hit " + rayHit);
					rayHit.transform.SetParent(gameObject.transform);
					CurrentlyHeldObject = rayHit.collider.gameObject;
					
					Debug.Log(rayHit.collider.gameObject);
					CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
					CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;

					CurrentlyHeldObject.layer = 2;
					CurrentlyHeldObject.transform.localPosition= new Vector3(0,0, 1.5f); 

					HoldingThing = true;
					return true;
				}
				else
				{
					Debug.Log("Can't pick up " + rayHit.transform.name + rayHit.transform.tag);
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
