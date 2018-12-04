using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PotUIFollow : MonoBehaviour
{
	public ContainerInventory pot;
	public Image iconA, iconB, iconC, statusIcon;

	public Sprite onion, tomato, greenCheck, redAlert;

	// Update is called once per frame
	void Update ()
	{
		transform.position = pot.transform.position + Vector3.up * 0.5f;
		
	}
}
