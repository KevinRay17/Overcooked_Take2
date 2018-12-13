using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{

	public Transform p1;
	public Transform p2;
	public Camera cam;
	
	//how many units we should keep from the players
	public float zoomFactor = 1.5f;
	public float followTimeDelta = .08f;
	
	// Update is called once per frame
	void Update ()
	{

		
		
		//midpoint between two players		
		Vector3 midpoint = (p1.position + p2.position) / 2f;

		//distance between the two players
		float distance = (p1.position - p2.position).magnitude;

		//move camera a certain distance
		Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
		
		Vector3 camMax = new Vector3(0,12.93f,-.64f);
		
		//new camera position
		cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

		if ((cameraDestination - cam.transform.position).magnitude <= .05f)
			
		{
			cam.transform.position = cameraDestination;
		}

		//Screen bounds y
		if (cam.transform.position.y > 15.93f)
			cam.transform.position = new Vector3(cam.transform.position.x, 15.93f, cam.transform.position.z);
		if (cam.transform.position.y < 13.93f)
			cam.transform.position = new Vector3(cam.transform.position.x, 13.93f, cam.transform.position.z);
		//screen bounds z
		if (cam.transform.position.z > 3f)
			cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 3f);
		if (cam.transform.position.z < .81f)
			cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, .81f);
		//screen bounds x
		if (cam.transform.position.x > 2.1f)
			cam.transform.position = new Vector3(2.1f, cam.transform.position.y, cam.transform.position.z);
		if (cam.transform.position.x < -.8f)
			cam.transform.position = new Vector3(-.8f, cam.transform.position.y, cam.transform.position.z);
		
	}
}
