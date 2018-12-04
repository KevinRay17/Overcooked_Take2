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
	
	
	//later: change rayhit.transform to rayhit.collider bc kevin's problem
	
	public KeyCode takeObject;

	public bool HoldingThing;
	

	//Depending on the system, maybe it should be a string array or just a bunch of tags
	public String[] acceptableTag;
	
	void Update () {

		
		if (Input.GetKeyDown(takeObject))
		{
			
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
	
	public void dropObjectCheck()
	{
		//1. raycast check to see if there is a pot in front of player
		//2. if yes, check to see if pot is full
		//3. If pot is not full, add vegetable to the array.
		//4. Destroy object
		
		
		//future edits: if you're holding a pot and you wanna hold a different pot, what do???
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.magenta);
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 1f)||
		    Physics.Raycast(transform.position-Vector3.down*0.75f, transform.TransformDirection(Vector3.forward), out rayHit, 1f)||
		    Physics.Raycast(transform.position-Vector3.down*0.5f, transform.TransformDirection(Vector3.forward), out rayHit, 1f))
		{
			Debug.Log("Hit " + rayHit.transform.name);
			if (rayHit.transform.GetComponent<MeshRenderer>().tag == "Pot")
			{
				
				if (CurrentlyHeldObject != null)
				{
					Debug.Log("holding smth and trying to interact w pot");
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
								HoldingThing = false;
								Destroy(temp);
								break;
							}
							
						}
						else
						{
							Debug.Log("tag doesnt match tag array");
						}
					}
				}
			}	
			else if(rayHit.transform.GetComponent<MeshRenderer>().tag == "Plate")
			{
				Debug.Log("Platehit");
				if (CurrentlyHeldObject.tag == "Pot" && CurrentlyHeldObject.GetComponent<ContainerInventory>().potFull)
				{
					Debug.Log("plat hit step 2");
					if (rayHit.transform.GetComponent<PlateInventory>().full)
					{
						Debug.Log("Plate is full");
					}
					else
					{
						//set plate to full
						rayHit.transform.GetComponent<PlateInventory>().full = true;
						Debug.Log("Plate is full: " + rayHit.transform.GetComponent<PlateInventory>().full);
						for (int i = 0; i < 3; i++)
						{
							CurrentlyHeldObject.GetComponent<ContainerInventory>().objectsInContainerIntVersion[i] = -1;
							CurrentlyHeldObject.GetComponent<ContainerInventory>().potFull = false;
							Debug.Log("plat full pot empty");
						}
					}
				}
			}
			else if(rayHit.transform.GetComponent<MeshRenderer>().tag ==  "Trash")
			{
				Debug.Log("hit trash");
				trash();
			}
		}
		
		else
		{
			dropObject();
		}
	}
	
	public void dropObject()
	{
		//1. set transform of CurrentlyHeldObject to be child of nothing
		//2. Set other object as not kinematic and apply gravity
		//3. set CurrentlyHeldObject as null
		
		if (CurrentlyHeldObject != null)
		{
			CurrentlyHeldObject.transform.SetParent(null);
			CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = true;
			CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = false;
			
			//check to see if object is round or not for the collider
			if(CurrentlyHeldObjectCode > 1)
			CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = true;
			else
			CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = true;
			
			
			CurrentlyHeldObject.layer = 0;

			CurrentlyHeldObject = null;
			HoldingThing = false;
			
			Debug.Log(CurrentlyHeldObject + "is null!!!!!!!!!!");
		}
	}

	public bool pickupObject()
	{
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.magenta);

		if (CurrentlyHeldObject == null)
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 1f)||
			    Physics.Raycast(transform.position-Vector3.down, transform.TransformDirection(Vector3.forward), out rayHit, 1f))
			{
				//fix this later to be more efficient
				for (int i = 0; i < acceptableTag.Length; i++)
				{
					if (rayHit.transform.GetComponent<MeshRenderer>().tag == acceptableTag[i])
					{
						Debug.Log("Hit " + rayHit);
						rayHit.transform.SetParent(this.transform);
						CurrentlyHeldObject = rayHit.transform.gameObject;
						CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
						CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;
						if (CurrentlyHeldObject.tag == "Tomato" || CurrentlyHeldObject.tag == "Onion")
						{
							CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = false;
						}

						CurrentlyHeldObject.layer = 2;
						
						HoldingThing = true;
						
						return true;
					}
					else
					{
						Debug.Log("Can't pick up " + rayHit.transform.name + rayHit.transform.tag);
					}
				}

			}
		}

		return false;
	}


	public void trash()
	{
		for (int i = 0; i < acceptableTag.Length; i++)
		{
			if (CurrentlyHeldObject.tag == acceptableTag[i])
			{
				Debug.Log("trash ofund tag");
				if (CurrentlyHeldObject.tag == "Pot")
				{
					if (CurrentlyHeldObject.GetComponent<ContainerInventory>().potFull)
					{
						//empty pot
						CurrentlyHeldObject.GetComponent<ContainerInventory>().emptyPot();
					}
				}else if (CurrentlyHeldObject.tag == "Plate")
				{
					if (CurrentlyHeldObject.GetComponent<PlateInventory>().full)
					{
						CurrentlyHeldObject.GetComponent<PlateInventory>().full = false;
					}
				}
				else
				{
					Debug.Log("throwaway");
					//destroy currently held object
					GameObject temp = CurrentlyHeldObject;
					CurrentlyHeldObject = null;
					HoldingThing = false;
					Destroy(temp);
				}
				break;
			}
		}
	}
	
	
	//For now: if you run into ab object, you pick it up
	//Future: if raycast hit an object, drop current object to pick it up

	private void OnCollisionEnter(Collision other)
	{
		
	}
}
