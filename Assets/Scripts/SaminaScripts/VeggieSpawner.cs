using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
//USAGE: put this code onto a Tomato Spawner
//Purpose: When a player gets close enough to the spawner, veggies spawn at the top of the crate
public class VeggieSpawner : MonoBehaviour {

	
	//vegetable prefabs that need to be spawned
	public GameObject Tomato;
	//spawn delay so there are not tomatoes flying around
	public float spawnDelay = 3f;
	private float nextSpawnTime;
	

	
	// Update is called once per frame
	void Update () {
		
}
	//Veggie Instantiate
	private void Spawn()
	{
		nextSpawnTime = Time.time + spawnDelay;
		Instantiate(Tomato, transform.position + new Vector3(0,1.5f,0), transform.rotation);
	}

	//is there anything already spawned?
	public void TrySpawn()
	{
		
		//define the ray: checking to see if there is already a veggie on the spawner
		Ray VeggieDetect = new Ray(transform.position, transform.up);
		
		//max distance from player
		float maxRaycastDist = 1f;
	
		//visualize the raycast
		Debug.DrawRay(VeggieDetect.origin, VeggieDetect.direction*maxRaycastDist, Color.cyan);
		//shoot the raycast, and if it returns true, spawn the veggie
		//if the spawn position is empty
		if(Physics.Raycast(VeggieDetect,maxRaycastDist)== false && Time.time >= nextSpawnTime)
		{
			Spawn();
		}
		
	}
}
//Maybe switch to raycast system later on?
//void OnTriggerEnter(Collider other)
//{
//switch system so that it goes thru an array of accepted string tags
//Maybe more efficient to check if pot is full first?
/*
 * for(int i = 0; i < tagList.length(); i++){
 * 		if(other.tag == tagList[i]){
 *			if(addVegetable(i)){
 *				add veggie
 * 			}
 * 		}
 * }
 *
if (other.tag== "Tomato")
{
	if (addVegetable(1))
	{
		Debug.Log("tomato added!!!");
		Destroy(other.gameObject);
	}
	else
	{
		Debug.Log("tomato failed to add!!!");
	}
}
else if (other.tag == "Onion")
{
	if (addVegetable(2))
	{
		Debug.Log("oinion added!!!");
		Destroy(other.gameObject);

	}
	else
	{
		Debug.Log("onion failed to add!!!");
	}
}	*/	
//}
