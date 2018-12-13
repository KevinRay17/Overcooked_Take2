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

		bool left = false, right = false, up = false, down = false;
		
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
					if (rayHit.transform.CompareTag("Table"))
					{
						Debug.Log("forward");
						up = true;
					}
				}

				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out rayHit, 2.2f))
				{
					if (rayHit.transform.CompareTag("Table"))
					{
						Debug.Log("backwards");
						down = true;
					}
				}

				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out rayHit, 2.2f))
				{
					if (rayHit.transform.CompareTag("Table"))
					{
						Debug.Log("left");
						left = true;
					}
				}

				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out rayHit, 2.2f))
				{
					if (rayHit.transform.CompareTag("Table"))
					{
						Debug.Log("Right");
						right = true;
					}
				}

				burnLifeCounter = 0;
				int randomDirection = Random.Range(0, 4);
				Vector3 position = transform.position;

				GameObject temp = Instantiate(firePrefab);
				if (randomDirection == 0 && down)
				{
					Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out rayHit, 2.2f);
					temp.transform.position += Vector3.back;
					Debug.Log(rayHit.transform.name);
					temp.transform.SetParent(rayHit.transform);
				}
				else if (randomDirection == 1 && up)
				{
					Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out rayHit, 2.2f);
					
					temp.transform.position += Vector3.forward;
					temp.transform.SetParent(rayHit.transform);
					Debug.Log(rayHit.transform.name);
				}
				else if (randomDirection == 2 && left)
				{
					Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out rayHit, 2.2f);
					temp.transform.position += Vector3.left;
					temp.transform.SetParent(rayHit.transform);
					Debug.Log(rayHit.transform.name);
				}
				else if (randomDirection == 3 && right)
				{
					Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out rayHit, 2.2f);
					temp.transform.position += Vector3.right;
					Debug.Log(rayHit.transform.name);
					temp.transform.SetParent(rayHit.transform);
				}
			}
			yield return wait;
		}
	}

	public void extinguish()
	{
		StartCoroutine(extinguishing());
	}

	IEnumerator extinguishing()
	{
		WaitForSeconds wait = new WaitForSeconds(1f);

		while (extinguishTimer < 10)
		{
			
			if (!beingExtinguished)
			{
				yield break;
			}
			extinguishTimer++;
			yield return wait;
		}

		beingExtinguished = false;
		Debug.Log("EXTINGUISHED FIRE!");
		Destroy(this);

	}
}
