using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneShow : MonoBehaviour
{

	public Text success, fail, total;
	
	public Image[] stars = new Image[3];
	
	// Use this for initialization
	void Start ()
	{
		success.text = OrderGeneration.pointCount/20 + " x 20= " + OrderGeneration.pointCount*2;
		fail.text = DishTimers.failedRecipes + " x 10 = " + DishTimers.failedRecipes * 10;

		total.text = OrderGeneration.pointCount * 2 - DishTimers.failedRecipes * 10 + "";
		StartCoroutine(starShow());
	}

	IEnumerator starShow()
	{
		WaitForSeconds wait = new WaitForSeconds(1);

		int starCount = 0;
		if (OrderGeneration.pointCount > 60)
		{
			if (DishTimers.failedRecipes < 1)
			{
				starCount = 3;
			}
			else
			{
				starCount = 2;
			}
		}
		else if (OrderGeneration.pointCount <=60 && OrderGeneration.pointCount >0)
		{
			if (DishTimers.failedRecipes < 3)
			{
				starCount = 2;
			}
			else
			{
				starCount = 1;
			}
		}
		else if (OrderGeneration.pointCount <= 0)
		{
			starCount = 1;
		}
		
		Color dark = stars[0].color;
		for (int i = 0; i < starCount; i++)
		{
			stars[i].color = Color.white;
		}
		yield return wait;
	}
}
