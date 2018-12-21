using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AI : MonoBehaviour {
	//Lifetime
	public float timerMax = 5f;

	public float timer = 0f;

	public Mesh[] Model;

	public List<Material> mats;
	// Use this for initialization
	void Start ()
	{
		int MeshNum = Random.Range(0, Model.Length);
		Mesh models = Instantiate(Model[MeshNum]);
		GetComponent<MeshFilter>().sharedMesh = models;
		List<Material> materials = GetComponent<MeshRenderer>().materials.ToList();
		if (MeshNum == 0)
		{
			materials.Clear();
			materials.Add(mats[0]);
			materials.Add(mats[1]);
			materials.Add(mats[2]);
			materials.Add(mats[3]);
			
			
			GetComponent<MeshRenderer>().materials = materials.ToArray();
		}

		else if (MeshNum==1)
		{
			materials.Clear();
			materials.Add(mats[4]);materials.Add(mats[5]);materials.Add(mats[6]);materials.Add(mats[7]);materials.Add(mats[8]); materials.Add(mats[9]);
			
			
		
			GetComponent<MeshRenderer>().materials = materials.ToArray();
		}
		else if (MeshNum==2)
		{ 
			materials.Clear();
			
			materials.Add(mats[10]);
			materials.Add(mats[11]);
			materials.Add(mats[12]);
			materials.Add(mats[13]);
			materials.Add(mats[14]);
			materials.Add(mats[15]);
			
			GetComponent<MeshRenderer>().materials = materials.ToArray();
		}
		transform.rotation = new Quaternion(0, 180, 0,0);
	}
	
	// Update is called once per frame
	void Update ()
	{

		timer += 1 * Time.deltaTime;
		//Movespeed
		Vector3 movement = new Vector3(0,0,.04f);
		transform.Translate(movement);
		//Kill
		if (timer >= timerMax)
		{
			Destroy(gameObject);
		}
	}
}
