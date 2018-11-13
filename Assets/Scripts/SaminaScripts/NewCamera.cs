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
		if (cam.transform.position.y > 12.93f)
			cam.transform.position = new Vector3(cam.transform.position.x, 12.93f, cam.transform.position.z);
		if (cam.transform.position.y < 11.93f)
			cam.transform.position = new Vector3(cam.transform.position.x, 11.93f, cam.transform.position.z);
		//screen bounds z
		if (cam.transform.position.z > 2.84f)
			cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 2.84f);
		if (cam.transform.position.z < -1.21f)
			cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -1.21f);
		//screen bounds x
		if (cam.transform.position.x > 1.5f)
			cam.transform.position = new Vector3(1.5f, cam.transform.position.y, cam.transform.position.z);
		if (cam.transform.position.x < -1.5f)
			cam.transform.position = new Vector3(-1.5f, cam.transform.position.y, cam.transform.position.z);
		
	}
}
