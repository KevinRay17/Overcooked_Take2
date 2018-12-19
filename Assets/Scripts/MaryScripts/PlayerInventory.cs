using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventory : MonoBehaviour
{
	private AudioSource myAudio;
	[FormerlySerializedAs("pickUpSFX")] public AudioClip dropSFX;
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
	public RaycastHit myRCH;

	public GameObject TomatoClone;
	public GameObject OnionClone;
	public ContainerInventory potInventory;

	public GameObject plate;
	public GameObject DishReturn;

	public GameObject Lighting;

	public OrderGeneration orderGen;
	public CuttingBoardScript cutBoard;

	//Depending on the system, maybe it should be a string array or just a bunch of tags
	public String[] acceptableTag;

	private void Start()
	{
		myAudio = GetComponent<AudioSource>();
	}

	void Update () {
		//Light something if if in raycast range so they know what they are selecting
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out myRCH, 2.2f))
		{
			if (myRCH.collider != null)
			{
				//Lighting.transform.SetParent(myRCH.collider.gameObject.transform);
				Lighting.transform.position = new Vector3(myRCH.collider.transform.position.x ,myRCH.collider.transform.position.y + 4.4f,myRCH.transform.position.z);
				Lighting.SetActive(true);
			}
		}
		else
		{
			Lighting.SetActive(false);
		}
	 
		
		if (Input.GetKeyDown(takeObject))
		{
			SnapToTable();
			
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
	//Empty pot/destroy object
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
						CurrentlyHeldObject.GetComponent<PlateInventory>().OnionSoup = false;
						CurrentlyHeldObject.GetComponent<PlateInventory>().TomatoSoup = false;
						CurrentlyHeldObject.GetComponent<PlateInventory>().isRuined = false;
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
			other.GetComponent<Transform>().SetParent(transform);
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
<<<<<<< HEAD
			if (rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Pot") || (rayHit.collider.gameObject.transform.childCount > 0 && rayHit.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Pot")))

		{
			
=======
			if (rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Pot") || (rayHit.collider.gameObject.CompareTag("Stove") && rayHit.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Pot")))
			{
				potInventory = rayHit.transform.GetComponent<ContainerInventory>();
>>>>>>> origin/Mary
				if (CurrentlyHeldObject != null)
				{
					Debug.Log("holding smth");
					//Check tag against array of tags
					string[] tempTag = potInventory.acceptedTag;
					for (int i = 0; i < tempTag.Length; i++)
					{
						//if tag is accepted, check container if can be added
						
						if (CurrentlyHeldObject.CompareTag(tempTag[i]))
						{
							Debug.Log("acceptable tag");
							//if accepted, destroy gameobject and reset currentlyheldobject and code
							if (potInventory.addVegetable(i) && !potInventory.burnt)
							{
								Debug.Log("pot can take");
								GameObject temp = CurrentlyHeldObject;
								CurrentlyHeldObject = null;
								HoldingThing = false;
								Destroy(temp);
								break;
							}
						}
					}
				}
			}
			else if (rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Plate") )
			{
				Debug.Log("Platehit");
<<<<<<< HEAD
				if (CurrentlyHeldObject.CompareTag("Pot") && CurrentlyHeldObject.GetComponent<ContainerInventory>().completelyFull && potInventory.cookCountDown <= 0)
=======
				if (CurrentlyHeldObject.tag == "Pot" && 
				    CurrentlyHeldObject.GetComponent<ContainerInventory>().completelyFull && 
				    potInventory.cookCountDown <= 0)
>>>>>>> origin/Mary
				{
					Debug.Log("plat hit step 2");
					if (rayHit.transform.GetComponent<PlateInventory>().full)
					{
						Debug.Log("Plate is full");
					}
					else
					{
						//set plate to the type of soup put in
						rayHit.transform.GetComponent<PlateInventory>().full = true;
						if (potInventory.objectsInContainerIntVersion[0] == 0 &&
						    potInventory.objectsInContainerIntVersion[1] == 0 &&
						    potInventory.objectsInContainerIntVersion[2] == 0 && !potInventory.burnt)
						{
							rayHit.transform.GetComponent<PlateInventory>().TomatoSoup = true;
						}
						else if (potInventory.objectsInContainerIntVersion[0] == 1 &&
						    potInventory.objectsInContainerIntVersion[1] == 1 &&
						    potInventory.objectsInContainerIntVersion[2] == 1 && !potInventory.burnt)
						{
							rayHit.transform.GetComponent<PlateInventory>().OnionSoup = true;
						}
						else
						{
							rayHit.transform.GetComponent<PlateInventory>().isRuined = true;	
						}

						for (int i = 0; i < 3; i++)
						{
							CurrentlyHeldObject.GetComponent<ContainerInventory>().emptyPot();
							//Debug.Log("plat full pot empty");
						}
					}
				}
			}
			//If plate is on a table and you want to put soup in it
			else if (rayHit.collider.gameObject.transform.childCount > 0)
			{
				if (rayHit.collider.gameObject.transform.GetChild(0).gameObject.transform.CompareTag("Plate"))
				{
					if (CurrentlyHeldObject.CompareTag("Pot") &&
					    CurrentlyHeldObject.GetComponent<ContainerInventory>().completelyFull &&
					    potInventory.cookCountDown <= 0)
					{
						Debug.Log("plat hit step 2");
						if (rayHit.collider.transform.GetChild(0).gameObject.GetComponent<PlateInventory>().full)
						{
							Debug.Log("Plate is full");
						}
						else
						{
							//set plate to the type of soup put in
							rayHit.collider.transform.GetChild(0).gameObject.GetComponent<PlateInventory>().full= true;
							if (potInventory.objectsInContainerIntVersion[0] == 0 &&
							    potInventory.objectsInContainerIntVersion[1] == 0 &&
							    potInventory.objectsInContainerIntVersion[2] == 0 && !potInventory.burnt)
							{
								rayHit.collider.transform.GetChild(0).gameObject.GetComponent<PlateInventory>().TomatoSoup = true;
							}
							else if (potInventory.objectsInContainerIntVersion[0] == 1 &&
							         potInventory.objectsInContainerIntVersion[1] == 1 &&
							         potInventory.objectsInContainerIntVersion[2] == 1 && !potInventory.burnt)
							{
								rayHit.collider.transform.GetChild(0).gameObject.GetComponent<PlateInventory>().OnionSoup= true;
							}
							else
							{
								rayHit.collider.transform.GetChild(0).gameObject.GetComponent<PlateInventory>().isRuined = true;
							}

							for (int i = 0; i < 3; i++)
							{
								CurrentlyHeldObject.GetComponent<ContainerInventory>().emptyPot();
								//Debug.Log("plat full pot empty");
							}
						}
					}
				}
			}
			else if(rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Trash"))
			{
				Debug.Log("hit trash");
				trash();
			}
			
			else if (rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Fire"))
			{
				if (CurrentlyHeldObject.CompareTag("Extinguisher"))
				{
					//Extinguish fire
					rayHit.transform.GetComponent<fireBehavior>().extinguish();
				}
			}
			else if (rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Server"))
			{
				if (CurrentlyHeldObject.CompareTag("Plate"))
				{
					OrderGeneration.OG.scoreCheck(CurrentlyHeldObject.GetComponent<PlateInventory>());
				}
			}
		}
		else
		{
			dropObject();
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
			//Check if player is holding the gameObject
			if (CurrentlyHeldObject.transform.IsChildOf(gameObject.transform))
			{
				CurrentlyHeldObject.transform.SetParent(null);
			}

			CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = true;
			CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = false;
			if (CurrentlyHeldObjectCode > 3)
			{
				CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = true;
			}
			else if (CurrentlyHeldObjectCode > 1 && CurrentlyHeldObjectCode < 3)
			{
				CurrentlyHeldObject.GetComponent<MeshCollider>().enabled =  true;
			}
			else
			{
				CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = true;
			}

			CurrentlyHeldObjectCode = 0;
			CurrentlyHeldObject = null;
			HoldingThing = false;
			//Debug.Log(CurrentlyHeldObject);
			myAudio.PlayOneShot(dropSFX);
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
				if (rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Pot"))
				{
					CurrentlyHeldObjectCode = 2;
				}

				else if (rayHit.transform.GetComponent<MeshRenderer>().CompareTag("Plate"))
				{
					CurrentlyHeldObjectCode = 4;
				}
				else
				{
					CurrentlyHeldObjectCode = 0;
				}
				bool isAcceptable = false;
				//fix this later to be more efficient
				for (int i = 0; i < acceptableTag.Length; i++)
				{ 
					if (rayHit.transform.GetComponent<MeshRenderer>().tag == acceptableTag[i])
					{
						isAcceptable = true;
						break;
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
					if (CurrentlyHeldObjectCode > 3)
					{
						CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = false;
					}
					else if (CurrentlyHeldObjectCode > 1 && CurrentlyHeldObjectCode < 3)
					{
						CurrentlyHeldObject.GetComponent<MeshCollider>().enabled = false;
					}
					else
					{
						CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = false;
					}

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
	
	
	void SnapToTable()
	{
		//Check what the RayCast hits and if the table is empty and you are holding an object, put held object on the table
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out myRCH, 2.2f)) {

			if ((myRCH.collider.gameObject.CompareTag("Table") || myRCH.collider.gameObject.CompareTag("TSpawner") || myRCH.collider.gameObject.CompareTag("OSpawner") 
			     || myRCH.collider.gameObject.CompareTag("CuttingBoard") || myRCH.collider.gameObject.CompareTag("CuttingBoard2")) && CurrentlyHeldObject != null && myRCH.collider.gameObject.transform.childCount == 0) {
				CurrentlyHeldObject.transform.SetParent(myRCH.collider.gameObject.transform);
				CurrentlyHeldObject.transform.localPosition = new Vector3(0,myRCH.collider.gameObject.transform.position.y +.5f,0);
				dropObjectCheck();
				dropObject();
				
			} else if (myRCH.collider.gameObject.CompareTag("Stove") && CurrentlyHeldObject != null &&
			           myRCH.collider.gameObject.transform.childCount == 0)
			{
				if (CurrentlyHeldObject.CompareTag("Pot"))
				{
					CurrentlyHeldObject.transform.SetParent(myRCH.collider.gameObject.transform);
					CurrentlyHeldObject.transform.localPosition =
						new Vector3(0, myRCH.collider.gameObject.transform.position.y + .5f, 0);
					dropObjectCheck();
					dropObject();
				}
			} 
			//Serve Dish if Plate is of correct order
			else if (myRCH.collider.gameObject.CompareTag("Server") && CurrentlyHeldObject != null)
			{
				if (CurrentlyHeldObject.CompareTag("Plate"))
				{
			
					if (CurrentlyHeldObject.GetComponent<PlateInventory>().isRuined == false)
<<<<<<< HEAD
					{ 
						if (CurrentlyHeldObject.GetComponent<PlateInventory>().TomatoSoup == true && orderGen.TomatoOrderCheck())
=======
					{
						if (OrderGeneration.OG.scoreCheck(CurrentlyHeldObject.GetComponent<PlateInventory>()))
						{
							Debug.Log("yay");
							StartCoroutine(DishReturnTimer());
							Destroy(CurrentlyHeldObject);
							CurrentlyHeldObjectCode = 0;
							CurrentlyHeldObject = null;
							HoldingThing = false;
						}

						/*
						if (CurrentlyHeldObject.GetComponent<PlateInventory>().TomatoSoup == true)
>>>>>>> origin/Mary
						{
							StartCoroutine(DishReturnTimer());
							Destroy(CurrentlyHeldObject);
							CurrentlyHeldObjectCode = 0;
							CurrentlyHeldObject = null;
							HoldingThing = false;
							orderGen.DestroyTomatoAndScore();
							//complete order and score
						}
						else if (CurrentlyHeldObject.GetComponent<PlateInventory>().OnionSoup == true && orderGen.OnionOrderCheck())
						{
							StartCoroutine(DishReturnTimer());
							Destroy(CurrentlyHeldObject);
							CurrentlyHeldObjectCode = 0;
							CurrentlyHeldObject = null;
							HoldingThing = false;
							orderGen.DestroyOnionAndScore();
							//complete order and score
						}*/
					}
				}
			}
			else if (myRCH.collider.gameObject.CompareTag("Dishwasher") && CurrentlyHeldObject != null)
			{
				if (CurrentlyHeldObject.CompareTag("Plate"))
				{
					if (CurrentlyHeldObject.GetComponent<PlateInventory>().isDirty == true)
					{
						CurrentlyHeldObject.transform.SetParent(myRCH.collider.gameObject.transform);
						CurrentlyHeldObject.transform.localPosition =
							new Vector3(1, myRCH.collider.gameObject.transform.position.y + .5f, 0);
						dropObjectCheck();
						dropObject();
					}
				}
			}
			
			



			//Pick Up from Spawner Tomato
			//If the Spawner has nothing on it then spawn new food and take it into hands
			else if (myRCH.collider.gameObject.CompareTag("TSpawner") && CurrentlyHeldObject == null && myRCH.collider.gameObject.transform.childCount == 0)
			{
				
				GameObject TClone = Instantiate(TomatoClone, new Vector3(0, 1, 0), Quaternion.identity);
				TClone.transform.SetParent(gameObject.transform);
				CurrentlyHeldObject = TClone.gameObject;
				CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
				CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;
				if (CurrentlyHeldObjectCode > 3)
				{
					CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = false;
				}
				else if (CurrentlyHeldObjectCode > 1 && CurrentlyHeldObjectCode < 3)
				{
					CurrentlyHeldObject.GetComponent<MeshCollider>().enabled = false;
				}
				else
				{
					CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = false;
				}

				CurrentlyHeldObject.layer = 2;
				CurrentlyHeldObject.transform.localPosition= new Vector3(0,0, 1.5f); 

				HoldingThing = true;
			}
			//Pick Up from Spawner Onion
			else if (myRCH.collider.gameObject.CompareTag("OSpawner") && CurrentlyHeldObject == null && myRCH.collider.gameObject.transform.childCount == 0)
			{
				GameObject OClone = Instantiate(OnionClone, new Vector3(0, 1, 0), Quaternion.identity);
				OClone.transform.SetParent(gameObject.transform);
				CurrentlyHeldObject = OClone.gameObject;
				CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
				CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;
				if (CurrentlyHeldObjectCode > 3)
				{
					CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = false;
				}
				else if (CurrentlyHeldObjectCode > 1 && CurrentlyHeldObjectCode < 3)
				{
					CurrentlyHeldObject.GetComponent<MeshCollider>().enabled = false;
				}
				else
				{
					CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = false;
				}
				CurrentlyHeldObject.layer = 2;
				CurrentlyHeldObject.transform.localPosition= new Vector3(0,0, 1.5f); 

				HoldingThing = true;
			}
			//If you aren't holding anything and the table has something on it, take it into hands
			else if ((myRCH.collider.gameObject.CompareTag("Table") || myRCH.collider.gameObject.CompareTag("TSpawner") ||
			     myRCH.collider.gameObject.CompareTag("OSpawner")
			     ||  myRCH.collider.gameObject.CompareTag("Stove") || myRCH.collider.gameObject.CompareTag("DishReturn")) && CurrentlyHeldObject == null &&
			    myRCH.collider.gameObject.transform.childCount > 0)
			{
				if (myRCH.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().CompareTag("Pot"))
				{
					CurrentlyHeldObjectCode = 2;
				}
				else if (myRCH.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>()
					.CompareTag("Plate"))
					CurrentlyHeldObjectCode = 4;
				else
				{
					CurrentlyHeldObjectCode = 0;
				}
				CurrentlyHeldObject = myRCH.collider.gameObject.transform.GetChild(0).gameObject;
				CurrentlyHeldObject.transform.SetParent(gameObject.transform);
				CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
				CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;
				if (CurrentlyHeldObjectCode > 3)
				{
					CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = false;
				}
				else if (CurrentlyHeldObjectCode > 1 && CurrentlyHeldObjectCode < 3)
				{
					CurrentlyHeldObject.GetComponent<MeshCollider>().enabled = false;
				}
				else
				{
					CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = false;
				}

				CurrentlyHeldObject.layer = 2;
				CurrentlyHeldObject.transform.localPosition= new Vector3(0,0, 1.5f); 


				HoldingThing = true;
				
			}
			//Can't pick up from cutting board if cutting started
			else if (myRCH.collider.gameObject.CompareTag("CuttingBoard") && CurrentlyHeldObject == null &&
			         myRCH.collider.gameObject.transform.childCount > 0  && (cutBoard.ChopBar1.fillAmount <=0))
			{
				if (myRCH.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().CompareTag("Pot"))
				{
					CurrentlyHeldObjectCode = 2;
				}
				else if (myRCH.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>()
					.CompareTag("Plate"))
					CurrentlyHeldObjectCode = 4;
				else
				{
					CurrentlyHeldObjectCode = 0;
				}

				CurrentlyHeldObject = myRCH.collider.gameObject.transform.GetChild(0).gameObject;
				CurrentlyHeldObject.transform.SetParent(gameObject.transform);
				CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
				CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;
				if (CurrentlyHeldObjectCode > 3)
				{
					CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = false;
				}
				else if (CurrentlyHeldObjectCode > 1 && CurrentlyHeldObjectCode < 3)
				{
					CurrentlyHeldObject.GetComponent<MeshCollider>().enabled = false;
				}
				else
				{
					CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = false;
				}

				CurrentlyHeldObject.layer = 2;
				CurrentlyHeldObject.transform.localPosition = new Vector3(0, 0, 1.5f);


				HoldingThing = true;
			

			}
			else if (myRCH.collider.gameObject.CompareTag("CuttingBoard2") && CurrentlyHeldObject == null &&
			         myRCH.collider.gameObject.transform.childCount > 0  && (cutBoard.ChopBar2.fillAmount <=0))
			{
				if (myRCH.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().CompareTag("Pot"))
				{
					CurrentlyHeldObjectCode = 2;
				}
				else if (myRCH.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>()
					.CompareTag("Plate"))
					CurrentlyHeldObjectCode = 4;
				else
				{
					CurrentlyHeldObjectCode = 0;
				}

				CurrentlyHeldObject = myRCH.collider.gameObject.transform.GetChild(0).gameObject;
				CurrentlyHeldObject.transform.SetParent(gameObject.transform);
				CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
				CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;
				if (CurrentlyHeldObjectCode > 3)
				{
					CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = false;
				}
				else if (CurrentlyHeldObjectCode > 1 && CurrentlyHeldObjectCode < 3)
				{
					CurrentlyHeldObject.GetComponent<MeshCollider>().enabled = false;
				}
				else
				{
					CurrentlyHeldObject.GetComponent<SphereCollider>().enabled = false;
				}

				CurrentlyHeldObject.layer = 2;
				CurrentlyHeldObject.transform.localPosition = new Vector3(0, 0, 1.5f);


				HoldingThing = true;
			

			}
		}
	}

	 IEnumerator DishReturnTimer()
	{
		WaitForSeconds wait = new WaitForSeconds(6);
		yield return wait;
		GameObject plateClone = Instantiate(plate, transform.position, Quaternion.identity);
		plateClone.transform.SetParent(DishReturn.gameObject.transform);
		plateClone.transform.localPosition = new Vector3(0,1,0);
		plateClone.GetComponent<PlateInventory>().isDirty = true;
	}
}
