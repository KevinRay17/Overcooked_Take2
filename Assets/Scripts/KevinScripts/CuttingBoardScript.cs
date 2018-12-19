using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Measure the Cutting of the Veggies in a separate script for clarity, can be put in playerInventory
public class CuttingBoardScript : MonoBehaviour {
	//Cutting Sound
	private AudioSource myAudio;

	public AudioClip Chop;
	//Button Press to cut food
	public KeyCode cutFood;
	
	//Raycast for cutting board
	public RaycastHit myRCH;
	//New Models on Cut
	public GameObject CutTomato;
	public GameObject CutOnion;


	public GameObject CuttingTomato1;
	public GameObject CuttingTomato2;
	public GameObject CuttingTomato3;
	public GameObject CuttingTomato4;
	public GameObject CuttingOnion1;
	public GameObject CuttingOnion2;
	public GameObject CuttingOnion3;
	public GameObject CuttingOnion4;

	//Reference the Player Inventory
	public PlayerInventory playerInventory;
   //Timers and reference to UI
	public float ChopTimer1 = 0;
	public float ChopTimer2 = 0;
	public float WashTimer = 0;
	public Image ChopBar1;
	public Image ChopBar2;
	public Image WashBar;
	static public float ChopMeter1 = 0;

	static public float ChopMeter2 = 0;

	static public float WashMeter = 0;
	// Use this for initialization
	void Start () {
		myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(cutFood))
		{
			ChoppingFood();
			
		}
	}

	void ChoppingFood()
	{
		ChopBar1.fillAmount = ChopMeter1/100;
		ChopBar2.fillAmount = ChopMeter2/100;
		WashBar.fillAmount = WashMeter / 100;
		
		//Check if Raycast hits an object and if that object is a cutting board and you are not holding anything and that cutting board has a child
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out myRCH, 2.2f))
		{
			if (myRCH.collider.gameObject.CompareTag("CuttingBoard") && (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato") || 
			                                                             myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion")) && 
			    playerInventory.CurrentlyHeldObject == null && myRCH.collider.gameObject.transform.childCount > 0)
			{
				//Rate at the Timer moves intervals then at 100 boost the real meter up a tick
				ChopTimer1 += 800 * (Time.deltaTime);
				if (ChopTimer1 >= 100)
				{
					myAudio.PlayOneShot(Chop);
					ChopMeter1 += 5;
					ChopTimer1 = 0;
				}
			}
			if (myRCH.collider.gameObject.CompareTag("CuttingBoard2") && (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato") || 
			                                                           myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion")) &&
			    playerInventory.CurrentlyHeldObject == null && myRCH.collider.gameObject.transform.childCount > 0)
			{
				ChopTimer2+= 800 * (Time.deltaTime);
				if (ChopTimer2 >= 100)
				{
					myAudio.PlayOneShot(Chop);
					ChopMeter2 += 5;
					ChopTimer2 = 0;
				}
			}

			if (myRCH.collider.gameObject.CompareTag("Dishwasher") &&
			    playerInventory.CurrentlyHeldObject == null && myRCH.collider.gameObject.transform.childCount > 0)
			{
				WashTimer += 800 * (Time.deltaTime);
				if (WashTimer >= 100)
				{
					WashMeter += 5;
					WashTimer = 0;
				}
			}

			//When the meter reaches 100 check the object it is and destroy it while instantiating a replacement CutFood
			if (ChopMeter1 >= 100)
			{
				if (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
				{
					Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
					GameObject CTomato = Instantiate(CutTomato, new Vector3(0, 1, 0), Quaternion.identity);
					CTomato.transform.SetParent(myRCH.collider.gameObject.transform);
					CTomato.transform.localPosition= new Vector3(0,1,0); 
				}
				else if (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
				{
					Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
					GameObject COnion = Instantiate(CutOnion, new Vector3(0, 1, 0), Quaternion.identity);
					COnion.transform.SetParent(myRCH.collider.gameObject.transform);
					COnion.transform.localPosition= new Vector3(0,1,0); 
				}

				ChopMeter1 = 0;
			}
			
			if (ChopMeter2 >= 100)
			{
				if (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
				{
					Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
					GameObject CTomato = Instantiate(CutTomato, new Vector3(0, 1, 0), Quaternion.identity);
					CTomato.transform.SetParent(myRCH.collider.gameObject.transform);
					CTomato.transform.localPosition= new Vector3(0,1,0); 
				}
				else if (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
				{
					Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
					GameObject COnion = Instantiate(CutOnion, new Vector3(0, 1, 0), Quaternion.identity);
					COnion.transform.SetParent(myRCH.collider.gameObject.transform);
					COnion.transform.localPosition= new Vector3(0,1,0); 
				}

				ChopMeter2 = 0;
			}
			
			//Change Models At Chop Parts 
			if (ChopMeter1 == 20 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion1 = Instantiate(CuttingOnion1, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion1.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			
			if (ChopMeter1 == 40 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion2 = Instantiate(CuttingOnion2, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion2.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter1 == 60 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion3 = Instantiate(CuttingOnion3, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion3.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter1 == 80 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion4 = Instantiate(CuttingOnion4, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion4.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			
			//Cutting Board 2 Onion
			if (ChopMeter2 == 20 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion1 = Instantiate(CuttingOnion1, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion1.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			
			if (ChopMeter2 == 40 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion2 = Instantiate(CuttingOnion2, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion2.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter2 == 60 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion3 = Instantiate(CuttingOnion3, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion3.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter2 == 80 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion"))
			{
				GameObject CutOnion4 = Instantiate(CuttingOnion4, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutOnion4.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			//Tomato Cutting
			if (ChopMeter1 == 20 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato1 = Instantiate(CuttingTomato1, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato1.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			
			if (ChopMeter1 == 40 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato2 = Instantiate(CuttingTomato2, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato2.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter1 == 60 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato3 = Instantiate(CuttingTomato3, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato3.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter1 == 80 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato4 = Instantiate(CuttingTomato4, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato4.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			//Cutting Board 2 Tomato
			if (ChopMeter2 == 20 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato1 = Instantiate(CuttingTomato1, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato1.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			
			if (ChopMeter2 == 40 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato2 = Instantiate(CuttingTomato2, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato2.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter2 == 60 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato3 = Instantiate(CuttingTomato3, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato3.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			if (ChopMeter2 == 80 && myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato"))
			{
				GameObject CutTomato4 = Instantiate(CuttingTomato4, new Vector3(
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.x,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.y,
					myRCH.collider.gameObject.transform.GetChild(0).gameObject.transform.position.z), Quaternion.identity);
				Destroy(myRCH.collider.gameObject.transform.GetChild(0).gameObject);
				CutTomato4.transform.SetParent(myRCH.collider.gameObject.transform);
			}
			
			
			//Add UI
			if (WashMeter >= 100 && myRCH.collider.gameObject.transform.childCount >0)
			{
				
				playerInventory.CurrentlyHeldObject = myRCH.collider.gameObject.transform.GetChild(0).gameObject;
				playerInventory.CurrentlyHeldObject.transform.SetParent(gameObject.transform);
				playerInventory.CurrentlyHeldObject.GetComponent<Rigidbody>().isKinematic = true;
				playerInventory.CurrentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;
				playerInventory.CurrentlyHeldObjectCode = 4;
				playerInventory.CurrentlyHeldObject.GetComponent<BoxCollider>().enabled = false;

				playerInventory.CurrentlyHeldObject.GetComponent<PlateInventory>().isDirty = false;
				playerInventory.CurrentlyHeldObject.layer = 2;
				playerInventory.CurrentlyHeldObject.transform.localPosition= new Vector3(0,0, 1.5f);
				WashMeter = 0;
				playerInventory.HoldingThing = true;
			}
			}
		}
		
	}

