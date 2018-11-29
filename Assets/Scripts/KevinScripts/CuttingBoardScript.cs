using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Measure the Cutting of the Veggies in a separate script for clarity, can be put in playerInventory
public class CuttingBoardScript : MonoBehaviour {
	//Button Press to cut food
	public KeyCode cutFood;
	
	//Raycast for cutting board
	public RaycastHit myRCH;
	//New Models on Cut
	public GameObject CutTomato;
	public GameObject CutOnion;
	//Reference the Player Inventory
	public PlayerInventory playerInventory;
   //Timers and reference to UI
	public float ChopTimer1 = 0;
	public float ChopTimer2 = 0;
	public Image ChopBar1;
	public Image ChopBar2;
	static public float ChopMeter1 = 0;

	static public float ChopMeter2 = 0;
	// Use this for initialization
	void Start () {
		
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
		
		//Check if Raycast hits an object and if that object is a cutting board and you are not holding anything and that cutting board has a child
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out myRCH, 2.2f))
		{
			if (myRCH.collider.gameObject.CompareTag("CuttingBoard") && (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato") || 
			                                                             myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion")) && 
			    playerInventory.CurrentlyHeldObject == null && myRCH.collider.gameObject.transform.childCount > 0)
			{
				//Rate at the Timer moves intervals then at 100 boost the real meter up a tick
				ChopTimer1 += 400 * (Time.deltaTime);
				if (ChopTimer1 >= 100)
				{
					ChopMeter1 += 5;
					ChopTimer1 = 0;
				}
			}
			if (myRCH.collider.gameObject.CompareTag("CuttingBoard2") && (myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Tomato") || 
			                                                           myRCH.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("Onion")) &&
			    playerInventory.CurrentlyHeldObject == null && myRCH.collider.gameObject.transform.childCount > 0)
			{
				ChopTimer2+= 400 * (Time.deltaTime);
				if (ChopTimer2 >= 100)
				{
					ChopMeter2 += 5;
					ChopTimer2 = 0;
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
		}
		
	}
}
