using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateInventory : MonoBehaviour
{

	public bool full;
	public bool OnionSoup;
	public bool TomatoSoup;
	public bool isRuined;
	public bool isDirty;
	public Mesh mesh;
	public Mesh emptyPlate;
	public Material Phong;
	public Material Metal;
	private void Update()
	{
		if (full)
		{
			Mesh PlateFull = Instantiate(mesh);
			GetComponent<MeshFilter>().sharedMesh = PlateFull;
			Material[] mats = GetComponent<MeshRenderer>().materials;
			mats[1] = Phong;
			GetComponent<MeshRenderer>().materials = mats;
		}
		else if (!full && !isDirty)
		{
			Mesh PlateEmpty = Instantiate(emptyPlate);
			GetComponent<MeshFilter>().sharedMesh = PlateEmpty;
			Material[] mats = GetComponent<MeshRenderer>().materials;
			mats[1] = Metal;
			GetComponent<MeshRenderer>().materials = mats;
		}
		
			
		
	}
}
