using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PotUIFollow : MonoBehaviour
{
	public ContainerInventory pot;
	public Image[] vegIcons;
	public Image statusIcon;
	public Sprite onion, tomato, greenCheck, redAlert;
<<<<<<< HEAD
	void Start()
	{
=======


	void Start()
	{
		statusIcon.enabled = false;
>>>>>>> origin/Mary
		for (int i = 0; i < 3; i++)
		{
			vegIcons[i].enabled = false;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		transform.position = pot.transform.position + Vector3.up/2 + Vector3.forward/2;
		if (pot.potFull)
		{
			for(int i = 0; i < 3; i++)
			{
				if (pot.objectsInContainerIntVersion[i] != -1)
				{
					vegIcons[i].enabled = true;
<<<<<<< HEAD
                    
=======
					
>>>>>>> origin/Mary
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
<<<<<<< HEAD
                
                
=======
				
				
>>>>>>> origin/Mary
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				vegIcons[i].enabled = false;
			}
		}
<<<<<<< HEAD
=======

>>>>>>> origin/Mary
		if (pot.cookCountDown == 0 && pot.burnTimer == 0)
		{
			statusIcon.enabled = true;
			statusIcon.sprite = greenCheck;
		}
		else if (pot.burnTimer > 0)
		{
			statusIcon.enabled = true;
			statusIcon.sprite = redAlert;
		}
		
		else
		{
<<<<<<< HEAD
            
=======
			statusIcon.enabled = false;
>>>>>>> origin/Mary
		}

		if (!pot.cooking)
		{
			statusIcon.enabled = false;
		}
	}
}