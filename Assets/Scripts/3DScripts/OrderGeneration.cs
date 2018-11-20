using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderGeneration : MonoBehaviour {

	public int orders;
	public int numberOfOrders;
	public int timeUntilNextOrder;
	public int orderTimer;


	// Use this for initialization
	void Start () {

		timeUntilNextOrder = 0;
		numberOfOrders = 0;

	}
	
	// Update is called once per frame
	void Update () {
		//Checks if the amount of active orders is at maximum, 6
		if (numberOfOrders <=6)
			{
			//checks if sufficient time has passed since the last order has popped up
				if (timeUntilNextOrder == 0)
				{
				//adds a new order, and resets the timer
				numberOfOrders += 1;
				timeUntilNextOrder = 180;
				}
			//keeps the timer constantly ticking down
			timeUntilNextOrder -= 1;
			}
	}
}
