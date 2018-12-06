using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBehavior : MonoBehaviour
{

	public bool alive;

	public int burnLifeCounter = 0;

	public int extinguishTimer = 0;
	
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
		if (burnLifeCounter < 20)
		{
			//raycast left and right to see if there is fire, if not, instantiate fire in 1 random direction
			
		}
		else
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 2.2f))
			{
				up = true;
			}

			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out rayHit, 2.2f))
			{
				down = true;
			}

			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out rayHit, 2.2f))
			{
			}

			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out rayHit, 2.2f))
			{
			}
			
		}

		if (alive)
		{
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
