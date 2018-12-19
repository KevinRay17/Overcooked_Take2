using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public Image CookMeter;

	public GameObject fire;
	
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
				
				Debug.Log(i);

				if (i == 0)
				{
					Debug.Log("new model");
					Destroy(transform.GetChild(0).gameObject);
					GameObject temp = Instantiate(potVariations[1], transform);
					temp.transform.localPosition = Vector3.zero;
				}else if (i == 2)
				{
					Debug.Log("new mode2l");
					Destroy(transform.GetChild(0).gameObject);
					GameObject temp = Instantiate(potVariations[2], transform);
					temp.transform.localPosition = Vector3.zero;
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
	//When pot is thrown away: empties the pot and returns it to empty prefab
	public void emptyPot()
	{
		for (int i = 0; i < objectsInContainerIntVersion.Length; i++)
		{
			objectsInContainerIntVersion[i] = -1;
			completelyFull = false;
			potFull = false;
			waitForBurn = 0;
			burnTimer = 0;
			burnt = false;
			enumRunning = false;
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
	
	//cooking countdown timer
	public IEnumerator Cooking()
	{
		WaitForSeconds wait = new WaitForSeconds(.1f);
		if (!enumRunning)
		{
			cooktime = 120;
			cookCountDown = cooktime;
			enumRunning = true;
		}

        
		while (potFull)
		{
			
                
			if (gameObject.transform.parent != null)
			{
				Debug.Log(gameObject.transform.parent.tag);
				if (gameObject.transform.parent.CompareTag("Stove") && cookCountDown > 0 && !burnt)
				{
					CookMeter.fillAmount = ((float) (cooktime - cookCountDown)) / cooktime;
					cookCountDown--;
					waitForBurn = 0;
					burnTimer = 0;
				}
				else if (gameObject.transform.parent.CompareTag("Stove") && cookCountDown <= 0 && waitForBurn < 60)
				{
					CookMeter.fillAmount = 0;
					waitForBurn++;
				}

				if (gameObject.transform.parent.CompareTag("Stove") && waitForBurn >= 60 && burnTimer < 120)
				{
					burnTimer++;
				}

				if (gameObject.transform.parent.CompareTag("Stove") && burnTimer >= 120)
				{
					burnt = true;
					//instantiate fire

					Debug.Log("instantiate fire");
					GameObject temp = Instantiate(fire,GetComponent<Transform>());
					temp.transform.localPosition = Vector3.zero;
					temp.transform.localScale = Vector3.one;
				}
			}

			yield return wait;
             
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
	
    
    
    /*Further implementation
     
     1. UI for the pots/bowls: show content based on whats in the inventory with icons above thing
     2. Pot transfer to bowl
        2a. bowl back to pot???
     3. Throw out content or transfer to bowl to get empty pot back
     4. What happens to player when object it hold is deleted? does it matter?
     5. Pot separate from content model so that content can change color without changing the whole pot
     
    */
    
    //cooking countdown timer
    
}

   
