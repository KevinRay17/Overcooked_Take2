using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	//Lifetime
	public float timerMax = 5f;

	public float timer = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += 1 * Time.deltaTime;
		//Movespeed
		Vector3 movement = new Vector3(0,0,-.025f);
		transform.Translate(movement);
		//Kill
		if (timer >= timerMax)
		{
			Destroy(gameObject);
		}
	}
}
