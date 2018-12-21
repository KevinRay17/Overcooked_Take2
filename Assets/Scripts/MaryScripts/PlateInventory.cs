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
	public Mesh dirtyPlate;
	public Material Phong;
	public Material Metal;
	public Material Rust;
	private void Update()
	{
		if (full)
		{
			Mesh PlateFull = Instantiate(mesh);
			GetComponent<MeshFilter>().sharedMesh = PlateFull;
			Material[] mats = GetComponent<MeshRenderer>().materials;
			mats[1] = Phong;
			mats[0] = Metal;
			GetComponent<MeshRenderer>().materials = mats;
		}
		else if (!full && !isDirty)
		{
			Mesh PlateEmpty = Instantiate(emptyPlate);
			GetComponent<MeshFilter>().sharedMesh = PlateEmpty;
			Material[] mats = GetComponent<MeshRenderer>().materials;
			mats[1] = Metal;
			mats[0] = Metal;
			GetComponent<MeshRenderer>().materials = mats;
		}
		else if (isDirty)
		{
			Mesh Rusty = Instantiate(dirtyPlate);
			GetComponent<MeshFilter>().sharedMesh = Rusty;
			Material[] mats = GetComponent<MeshRenderer>().materials;
			mats[0] = Rust;
			mats[1] = Metal;
			GetComponent<MeshRenderer>().materials = mats;
		}
			
		
	}
}
