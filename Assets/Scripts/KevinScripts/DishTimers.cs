using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishTimers : MonoBehaviour
{
	public Image Timer;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Timer.fillAmount -= .01f * Time.deltaTime;
		if (Timer.fillAmount <= 0)
		{
			DestroyAndScore();
		}
	}

	public void DestroyAndScore()
	{
		Destroy(transform.parent.gameObject);
	}
}
