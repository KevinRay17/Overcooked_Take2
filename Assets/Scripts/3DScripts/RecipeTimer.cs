using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTimer : MonoBehaviour
{
	public int timeleft;

	// Use this for initialization
	void Start ()
	{
		timeleft = 1500;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeleft -= 1;

		if (timeleft == 0)
		{
			GameObject scorekeeper = GameObject.Find("GameTime");

			OrderGeneration.OG.pointCount -= OrderGeneration.OG.recipePrice;
			OrderGeneration.OG.numberOfOrders -= 1;
			OrderGeneration.OG.failedRecipes++;
			
			GameObject[] orders;
			orders = GameObject.FindGameObjectsWithTag("Recipe");
			foreach (GameObject order in orders)
			{
				order.transform.position -= new Vector3(250f, transform.position.y, 0f);
			}

			Destroy(gameObject);
		}

	}
}