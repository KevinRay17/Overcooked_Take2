using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {
	public float timerMax = 0;

	public float timer = 0;

	public GameObject AI;
	// Use this for initialization
	void Start ()
	{
		//Initiate time to spawn
		timerMax = Random.Range(2f, 5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Move timer
		timer += 1 * Time.deltaTime;
		//When timer reaches max, spawn AI and reset timer and change time to spawn
		if (timer >= timerMax)
		{
			Instantiate(AI, transform.position, Quaternion.identity);
			timer = 0;
			timerMax = Random.Range(2f, 5f);
		}
	}
}
