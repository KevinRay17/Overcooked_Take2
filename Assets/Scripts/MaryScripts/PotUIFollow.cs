using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotUIFollow : MonoBehaviour
{
	public ContainerInventory pot;
	//public SpriteRenderer[] vegIcons;
	public Image[] vegIcons;
	public Image statusIcon;

	public Sprite onion, tomato, greenCheck, redAlert;


	// Update is called once per frame
	void Update ()
	{
		transform.position = pot.transform.position + Vector3.up + Vector3.forward*2;
		transform.LookAt(Camera.main.transform);
		if (pot.potFull)
		{
			for(int i = 0; i < 3; i++)
			{
				if (pot.objectsInContainerIntVersion[i] != -1)
				{
					vegIcons[pot.objectsInContainerIntVersion[i]].enabled = true;

					if (pot.objectsInContainerIntVersion[i] == 0)
					{
						vegIcons[i].sprite  = tomato;
					}
					else if (pot.objectsInContainerIntVersion[i] == 1)
					{
						vegIcons[i].sprite = onion;
					}
				}
				else
				{
					vegIcons[i].enabled = false;
				}
			}
		}

		if (pot.cooktime == 0)
		{
			statusIcon.sprite = greenCheck;
		}
		else if (pot.waitForBurn >= 60 && pot.burnTimer < 120)
		{
			statusIcon.sprite = redAlert;
		}
		else if (pot.burning)
		{
			
		}
		else
		{
			statusIcon.enabled = false;
		}
	}
}
