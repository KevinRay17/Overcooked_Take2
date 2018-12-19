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
		success.text = OrderGeneration.OG.successRecipes + " x 20 = " + OrderGeneration.OG.successRecipes*20;
		fail.text = OrderGeneration.OG.failedRecipes + " x 10 = " + OrderGeneration.OG.failedRecipes * 10;

		total.text = OrderGeneration.OG.successRecipes * 20 - OrderGeneration.OG.failedRecipes * 10 + "";
		StartCoroutine(starShow());
	}

	IEnumerator starShow()
	{
		WaitForSeconds wait = new WaitForSeconds(1);

		int starCount = 0;
		if (OrderGeneration.OG.successRecipes > 7)
		{
			if (OrderGeneration.OG.failedRecipes < 1)
			{
				starCount = 3;
			}
			else
			{
				starCount = 2;
			}
		}
		else if (OrderGeneration.OG.successRecipes <=7)
		{
			if (OrderGeneration.OG.failedRecipes < 5)
			{
				starCount = 2;
			}
			else
			{
				starCount = 1;
			}
		}
		else
		{
			starCount = 0;
		}
		
		Color dark = stars[0].color;
		for (int i = 0; i < starCount; i++)
		{
			stars[i].color = Color.Lerp(dark,Color.white, Mathf.PingPong(Time.time,1));
		}
		yield return wait;
	}
}
