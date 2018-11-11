using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventory : MonoBehaviour {

	//Maybe shouldn't use list of gameobejcts bc you cant take something out of the pot


	public GameObject[] potVariations;
	
	public List<GameObject> objectsInContainer;

	//Change this to a better name later lol
	public int[] objectsInContainerIntVersion = new int[3];

	public string[] acceptedTag = new string[2];
	
	
	
	//sort objects by tag

	void Start ()
	{
		objectsInContainer = new List<GameObject>();
	}

	//Maybe switch to raycast system later on?
	void OnTriggerEnter(Collider other)
	{
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
		 */
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
		}		
	}

	//tries to add the veggie to the pot and returns true if successful; if pot is full, returns false;
	public bool addVegetable(int vegetable)
	{
		
		for (int i = 0; i < objectsInContainerIntVersion.Length; i++)
		{
			if (objectsInContainerIntVersion[i] == 0)
			{
				objectsInContainerIntVersion[i] = vegetable;
				return true;
			}
		}
		Debug.Log("'uh oh its full,' says add vegetable");
		return false;
	}

	//When pot is thrown away: empties the pot and returns it to empty prefab
	private void emptyPot()
	{
		for (int i = 0; i < objectsInContainerIntVersion.Length; i++)
		{
			objectsInContainerIntVersion[i] = 0;
		}
	}
	
	/*Further implementation
	 
	 1. UI for the pots/bowls: show content based on whats in the inventory with icons above thing
	 2. Pot transfer to bowl
		2a. bowl back to pot???
	 3. Throw out content or transfer to bowl to get empty pot back
	 4. What happens to player when object it hold is deleted? does it matter?
	 5. Pot separate from content model so that content can change color without changing the whole pot
	 
    */
}
