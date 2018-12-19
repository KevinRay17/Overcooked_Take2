using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeep : MonoBehaviour {

	public Text score;
	public static int pointCount = 0;
	public int recipePrice;

	// Use this for initialization
	void Start () {
		recipePrice = 24;
	}
	
	// Update is called once per frame
	void Update () {

		score.text = " " + pointCount;

		if (Input.GetKeyDown("space"))
		{
			pointCount += recipePrice;
		}
			
	}
}
