using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBehavior : MonoBehaviour
{

	//1. Set itself as a child of the table
	//2. snap to table's position
	//3. raycast all 4 directions to see if there's a table (if rayhit.tag == table/stove/counter or something)
	public bool alive;
	public bool beingExtinguished;

	public int burnLifeCounter = 0;

	public int extinguishTimer = 0;

	public GameObject firePrefab;
	
	// Use this for initialization
	void Start ()
	{
		alive = true;
		StartCoroutine(FireSpread());
	}


	public IEnumerator FireSpread()
	{
		 Ray ray = new Ray();
		 RaycastHit rayHit = new RaycastHit();

		bool left, right, up, down;
		
		WaitForSeconds wait = new WaitForSeconds(1);
		while (alive)
		{
			if (burnLifeCounter < 30 && !beingExtinguished)
			{
				//raycast left and right to see if there is fire, if not, instantiate fire in 1 random direction
				burnLifeCounter++;
			}
			else if (burnLifeCounter == 30 && !beingExtinguished)
			{
				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit,
					2.2f))
				{
					up = true;
				}

				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out rayHit, 2.2f))
				{
					down = true;
				}

				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out rayHit, 2.2f))
				{
					left = true;
				}

				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out rayHit, 2.2f))
				{
					right = true;
				}

				burnLifeCounter = 0;
				int randomDirection = Random.Range(0, 4);
				Vector3 position = transform.position;

				GameObject temp = Instantiate(firePrefab);
				if (randomDirection == 0)
				{
					temp.transform.position += Vector3.down;
				}
				else if (randomDirection == 1)
				{
					temp.transform.position += Vector3.forward;
				}
				else if (randomDirection == 2)
				{
					temp.transform.position += Vector3.left;
				}
				else if (randomDirection == 3)
				{
					temp.transform.position += Vector3.right;
				}
			}
			yield return wait;
		}
	}

	public bool extinguish()
	{
		bool isExtinguished = false;

		extinguishTimer++;

		if (extinguishTimer < 10)
		{
			isExtinguished = true;
		}

		return isExtinguished;
	}
}
