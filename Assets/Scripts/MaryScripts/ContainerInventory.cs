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
	public bool cooking, cooked, overcooked, burning;

	//Change this to a better name later lol
	public int[] objectsInContainerIntVersion = new int[3];
	
	public string[] acceptedTag = new string[2];
	
	public int waitForBurn = 0;
	public int burnTimer = 0;
	public bool burnt = false;
	
	//use for checking dish Orders
	public bool completelyFull;
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
     
		     //chnage the cooking system to check if pot is on stove!!!
     		WaitForSeconds wait = new WaitForSeconds(1);
     		cookCountDown = cooktime;

		     cooking = true;
		     
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
			     cooked = true;
			     StartCoroutine(burnTimerCountdown());
     			Debug.Log("burn timer starting");
     		}
     
     	}
	public IEnumerator burnTimerCountdown()
	{

		int burnTime = 30;
		WaitForSeconds wait = new WaitForSeconds(1);

		enumRunning = true;
		if (cookCountDown > 0)
		{
			Debug.Log("burning time" + burnTime);
			//if you take the pot off the fire, then stop burning
			if (!burning)
			{
				yield break;
			}
			while (potFull)
			{
				burnTime--;

				yield return wait;
			}
		}
		else
		{
			overcooked = true;
			Debug.Log("AHHH BURNING FIRE");
		}

	}

	public IEnumerator potFireTimer()
	{
		WaitForSeconds wait = new WaitForSeconds(1);
		yield return wait;
	}
}
