using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Timer : MonoBehaviour {

	public int timeLeft;
	public Text timer;
	public GameObject gameOver;
	public int minutesLeft;
	public int secondsLeft;

	// Use this for initialization
	void Start () {
		secondsLeft = 3600;
		minutesLeft = 3;
	}
	
	// Update is called once per frame
	void Update () {


		if (minutesLeft >= 0 )
		{
			secondsLeft -= 1;
		}

		if (secondsLeft > 600)
		{
			timer.text = minutesLeft + ":" + secondsLeft/60;
		}



		if (secondsLeft < 600)
		{
			timer.text = minutesLeft + ":0" + secondsLeft/60;
		}


		if (secondsLeft == 0)
		{
			minutesLeft -=1;
			secondsLeft = 3540;
		}

		if (minutesLeft == -1 )
		{
			gameOver.SetActive(true);
			timer.text = "0:00";
			secondsLeft = 1;

		}


	}
}