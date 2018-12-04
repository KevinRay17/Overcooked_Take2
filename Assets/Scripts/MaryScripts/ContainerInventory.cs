using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventory : MonoBehaviour {

	//Maybe shouldn't use list of gameobejcts bc you cant take something out of the pot


	public int cooktime = 45;
	public int cookCountDown = 900;
	
	public GameObject[] potVariations;
	
	public List<GameObject> objectsInContainer;

	//use this to start the enum cooking
	public bool potFull = false;
	private bool enumRunning;

	//Change this to a better name later lol
	public int[] objectsInContainerIntVersion = new int[3];
	
	public string[] acceptedTag = new string[2];
	
	
	
	//sort objects by tag

	void Start ()
	{
		objectsInContainerIntVersion[0] = -1;
		objectsInContainerIntVersion[1] = -1;
		objectsInContainerIntVersion[2] = -1;
		objectsInContainer = new List<GameObject>();
	}

	//tries to add the veggie to the pot and returns true if successful; if pot is full, returns false;
	public bool addVegetable(int vegetable)
	{
		
		for (int i = 0; i < objectsInContainerIntVersion.Length; i++)
		{
			if (objectsInContainerIntVersion[i] == -1)
			{
				if (!potFull)
				{
					potFull = true;
				}
				
				objectsInContainerIntVersion[i] = vegetable;
				
				if (enumRunning)
				{
					Debug.Log("more time");
					cookCountDown += 15;
				}
				else
				{
					Debug.Log("started cooking");
					StartCoroutine(Cooking());
				}
				
				return true;
			}
		}
		Debug.Log("'uh oh its full,' says add vegetable");
		return false;
	}

	//When pot is thrown away: empties the pot and returns it to empty prefab
	public void emptyPot()
	{
		for (int i = 0; i < objectsInContainerIntVersion.Length; i++)
		{
			objectsInContainerIntVersion[i] = -1;
		}

		potFull = false;
	}
	
	/*Further implementation
	 
	 1. UI for the pots/bowls: show content based on whats in the inventory with icons above thing
	 2. Pot transfer to bowl
		2a. bowl back to pot???
	 3. Throw out content or transfer to bowl to get empty pot back
	 4. What happens to player when object it hold is deleted? does it matter?
	 5. Pot separate from content model so that content can change color without changing the whole pot
	 
    */
	
	//cooking countdown timer
	public IEnumerator Cooking()
	{

		WaitForSeconds wait = new WaitForSeconds(1);
		cookCountDown = cooktime;

		enumRunning = true;
		if (cookCountDown > 0)
		{
			Debug.Log("cooking time" + cookCountDown);
			while (potFull)
			{
				cookCountDown--;

				yield return wait;
			}
		}
		else
		{
			Debug.Log("AHHH BURNING FIRE");
		}

	}
}
