using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishTimers : MonoBehaviour
{
	private AudioSource myAudio;
	public AudioClip BWAH;
	public Image Timer;
	public static int failedRecipes;

	// Use this for initialization
	void Start ()
	{
		myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Timer.fillAmount -= .01f * Time.deltaTime;
		if (Timer.fillAmount <= 0)
		{
			DestroyAndScore();
		}
	}

	public void DestroyAndScore()
	{
		myAudio.PlayOneShot(BWAH);
		failedRecipes++;
		Destroy(transform.parent.gameObject);
	}
}
