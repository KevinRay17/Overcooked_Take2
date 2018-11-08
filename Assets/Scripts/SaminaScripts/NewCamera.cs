using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{

	public Transform p1;
	public Transform p2;
	public Camera cam;
	
	// Update is called once per frame
	void Update ()
	{

		//how many units we should keep from the players
		float zoomFactor = 1.5f;
		float followTimeDelta = .08f;

		//midpoint between two players		
		Vector3 midpoint = (p1.position + p2.position) / 2f;

		//distance between the two players
		float distance = (p1.position - p2.position).magnitude;

		//move camera a certain distance
		Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
		
		//new camera position
		cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

		if ((cameraDestination - cam.transform.position).magnitude <= .05f)
		{
			cam.transform.position = cameraDestination;
		}
	}
}
