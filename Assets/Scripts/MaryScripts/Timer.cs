using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
		StartCoroutine(countdownTimer());
	}

	IEnumerator countdownTimer()
	{
		WaitForSeconds wait = new WaitForSeconds(1);
		while (secondsLeft > 0)
		{
			secondsLeft--;
			float minutes = Mathf.Floor(secondsLeft / 60);
			int seconds = Mathf.RoundToInt(secondsLeft%60);
			timer.text = minutes + ":" + seconds;
			yield return wait;
		}
		
		SceneManager.LoadScene("EndScene");
	}
}