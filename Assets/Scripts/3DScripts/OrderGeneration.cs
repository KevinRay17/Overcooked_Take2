using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderGeneration : MonoBehaviour {

	public Text score;
	public int pointCount = 0;
	public int recipePrice;

	public int failedRecipes = 0;
	public int successRecipes = 0;
	
	public int numberOfOrders;
	public int timeUntilNextOrder;
	public GameObject recipe1Prefab;
	public GameObject recipe2Prefab;
	public Canvas gameCanvas;

	public int whichRecipe;

	public static OrderGeneration OG;
	
	public int [] orderArray = new int[6];


	// Use this for initialization
	void Start () {

		DontDestroyOnLoad(this);
		recipePrice = 24;
		

		timeUntilNextOrder = 100;
		numberOfOrders = 0;

		if (OG == null)
		{
			OG = this;
		}
		else
		{
			Destroy(gameObject);
		}

		for (int i = 0; i < orderArray.Length; i++)
		{
			orderArray[i] = -1;
		}

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

					for (int i = 0; i < orderArray.Length; i++)
					{
						if (orderArray[i] == -1)
						{
							orderArray[i] = whichRecipe;
							break;
						}
					}
				
				timeUntilNextOrder = 280;
				}
			//keeps the timer constantly ticking down
			timeUntilNextOrder -= 1;
			}
		
		
		//displays the player's score
		score.text = " " + pointCount;

		
	}


	public bool scoreCheck(PlateInventory PI)
	{
		for (int i = 0; i < orderArray.Length; i++)
		{
			if (orderArray[i] != -1)
			{
				if (PI.OnionSoup || PI.TomatoSoup)
				{
					successRecipes++;
					orderArray[i] = -1;
					numberOfOrders--;
					return true;
				}
			}
		}

		return false;
	}
}
