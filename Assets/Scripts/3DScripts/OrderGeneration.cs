using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrderGeneration : MonoBehaviour {

	public Text score;
	public int pointCount = 0;
	public int recipePrice;

<<<<<<< HEAD
	public bool tomatoOrder;
	public bool onionOrder;
=======
	public int failedRecipes = 0;
	public int successRecipes = 0;
>>>>>>> origin/Mary
	
	public int numberOfOrders;
	public  float timeUntilNextOrder;
	public GameObject recipe1Prefab;
	public GameObject recipe2Prefab;
	public Canvas gameCanvas;

	public int whichRecipe;

<<<<<<< HEAD
	public List<GameObject> _orderList;
	
	//public GameObject [] orderArray = new GameObject[6];
	


	// Use this for initialization
	void Start ()
	{
		_orderList = new List<GameObject>();
		pointCount = 0;
=======
	public static OrderGeneration OG;
	
	public int [] orderArray = new int[6];


	// Use this for initialization
	void Start () {
>>>>>>> origin/Mary

		DontDestroyOnLoad(this);
		recipePrice = 24;
		

		timeUntilNextOrder = 10;
		numberOfOrders = 0;

		if (OG == null)
		{
			OG = this;
		}
		else
		{
			Destroy(gameObject);
		}

		for (int i = 0; i < orderArray.Length; i++)
		{
			orderArray[i] = -1;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		//Checks if the amount of active orders is at maximum, 6
		for (int i = 0; i < 5; i++)
		{
			if (numberOfOrders < 5)
			{
				//checks if sufficient time has passed since the last order has popped up
				if (timeUntilNextOrder <= 0)
				{
					//adds a new order, and resets the timer
					numberOfOrders += 1;
					//randomly choose between choosing the onion soup and tomato soup
					whichRecipe = Random.Range(1, 3);

					//if int is 1, spawn onion soup
					if (whichRecipe == 1)
					{

						GameObject recipeInstance1 = Instantiate(recipe1Prefab,
							transform.position + new Vector3(-20 + (40 * numberOfOrders), 10f), Quaternion.identity);
						_orderList.Add(recipeInstance1);
						recipeInstance1.transform.SetParent(gameCanvas.transform);
						recipeInstance1.GetComponent<RectTransform>().anchoredPosition =
							new Vector3(-445 + (80 * numberOfOrders), 180f, 0);
						recipeInstance1.transform.localPosition = new Vector3(recipeInstance1.transform.localPosition.x,
							recipeInstance1.transform.localPosition.y, 0);
						recipeInstance1.transform.localEulerAngles = new Vector3(0, 0, 0);
						recipeInstance1.transform.localScale = new Vector3(1, 1, 1);
					}

					//if int is 2, spawn tomato soup
					else if (whichRecipe == 2)
					{
						GameObject recipeInstance2 = Instantiate(recipe2Prefab,
							transform.position + new Vector3(-20 + (40 * numberOfOrders), 10f), Quaternion.identity);
						_orderList.Add(recipeInstance2);
						recipeInstance2.transform.SetParent(gameCanvas.transform);
						recipeInstance2.GetComponent<RectTransform>().anchoredPosition =
							new Vector3(-445 + (80 * numberOfOrders), 180f, 0);
						recipeInstance2.transform.localPosition = new Vector3(recipeInstance2.transform.localPosition.x,
							recipeInstance2.transform.localPosition.y, 0);
						recipeInstance2.transform.localEulerAngles = new Vector3(0, 0, 0);
						recipeInstance2.transform.localScale = new Vector3(1, 1, 1);

					}

<<<<<<< HEAD
					timeUntilNextOrder = Random.Range(35, 100);
=======
					for (int i = 0; i < orderArray.Length; i++)
					{
						if (orderArray[i] == -1)
						{
							orderArray[i] = whichRecipe;
							break;
						}
					}
				
				timeUntilNextOrder = 280;
>>>>>>> origin/Mary
				}

				//keeps the timer constantly ticking down
				timeUntilNextOrder -= 1 *Time.deltaTime;
				
			}
		}


		//displays the player's score
		score.text = " " + pointCount;

<<<<<<< HEAD
		//Remove empty items in list
		for(int i = _orderList.Count - 1; i > -1; i--)
		{
			if (_orderList[i] == null)
			{
				
				_orderList.RemoveAt(i);
				int x = i;
				while (x < _orderList.Count)
				{
					_orderList[x].transform.localPosition -= new Vector3(80, 0, 0);
					x++;
				}

				//_orderList.RemoveAt(i);
				numberOfOrders--;
			}
			
		}
		//placeholder for incrementing the player's score when a dish is served
		if (Input.GetKeyDown("space"))
=======
		
	}


	public bool scoreCheck(PlateInventory PI)
	{
		for (int i = 0; i < orderArray.Length; i++)
>>>>>>> origin/Mary
		{
			if (orderArray[i] != -1)
			{
				if (PI.OnionSoup || PI.TomatoSoup)
				{
					successRecipes++;
					orderArray[i] = -1;
					numberOfOrders--;
					return true;
				}
			}
		}

		return false;
	}

	public bool TomatoOrderCheck()
	{
		foreach (var i in _orderList)
		{
			if (i.CompareTag("TomatoTimer"))
			{
				return true;
			}
		}
		return false;
	}
	public bool OnionOrderCheck()
	{
		foreach (var i in _orderList)
		{
			if (i.CompareTag("OnionTimer"))
			{
				return true;
			}
		}
		return false;
	}
	public void DestroyOnionAndScore()
	{
		foreach (var i in _orderList)
		{
			if (i.CompareTag("OnionTimer"))
			{
				pointCount += Mathf.RoundToInt(100 * i.GetComponentInChildren<DishTimers>().Timer.fillAmount);
				Destroy(i);
				break;
			}
		}
	}
	public void DestroyTomatoAndScore()
	{
		foreach (var i in _orderList)
		{
			if (i.CompareTag("TomatoTimer"))
			{
				
				pointCount += Mathf.RoundToInt(100 * i.GetComponentInChildren<DishTimers>().Timer.fillAmount);
				Destroy(i);
				break;
			}
		}
	}
}
