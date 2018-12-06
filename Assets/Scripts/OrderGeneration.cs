using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderGeneration : MonoBehaviour {

	public Text score;
	public int pointCount;
	public int recipePrice;
	
	public int numberOfOrders;
	public int timeUntilNextOrder;
	public GameObject recipe1Prefab;
	public GameObject recipe2Prefab;
	public Canvas gameCanvas;

	public int whichRecipe;
	
	//public GameObject [] orderArray = new GameObject[6];


	// Use this for initialization
	void Start () {
		
		pointCount = 0;

		recipePrice = 24;
		

		timeUntilNextOrder = 100;
		numberOfOrders = 0;

		//for (int i = 0; i < orderArray.Length; i++)

	}
	
	// Update is called once per frame
	void Update () {
		//Checks if the amount of active orders is at maximum, 6
		if (numberOfOrders <5)
			{
			//checks if sufficient time has passed since the last order has popped up
				if (timeUntilNextOrder == 0)
				{
				//adds a new order, and resets the timer
				numberOfOrders += 1;
					//randomly choose between choosing the onion soup and tomato soup
					whichRecipe = Random.Range(1, 3);
					
					//if int is 1, spawn onion soup
					if (whichRecipe == 1)
					{
						GameObject recipeInstance1 = Instantiate (recipe1Prefab, transform.position + new Vector3(250f * numberOfOrders, -100f), transform.rotation);
						recipeInstance1.transform.SetParent(gameCanvas.transform);
					}
					
					//if int is 2, spawn tomato soup
					else if (whichRecipe == 2)
					{
						GameObject recipeInstance1 = Instantiate (recipe2Prefab, transform.position + new Vector3(250f * numberOfOrders, -100f), transform.rotation);
						recipeInstance1.transform.SetParent(gameCanvas.transform);
					}
				
				timeUntilNextOrder = 280;
				}
			//keeps the timer constantly ticking down
			timeUntilNextOrder -= 1;
			}
		
		
		//displays the player's score
		score.text = " " + pointCount;

		
		//placeholder for incrementing the player's score when a dish is served
		if (Input.GetKeyDown("space"))
		{
			pointCount += recipePrice;
		}
		
		
	}
}