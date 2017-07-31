using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarRaceLogic : MonoBehaviour {

	//TODO Add push to screen system like in LDJ 38
	//TODO Pick name and add it to player settings

	EdgeCollider2D[] checkpoints;
	PopupText popupText;
	int lastVisitedCheckpoint = 0;
	int visitedCheckpoints = -1;

	float elapsedTime;
	float[] lapTimes;
	int bestLap = -1;

	public int currentLap = 1;
	public int totalLaps = 3;

	public AudioClip countdownSound;

	public float TotalElapsedTime {
		get {
			float sum = elapsedTime;
			for (int i = 0; i < currentLap; i++) sum += lapTimes[i];
			return sum;
		}
	}

	public float CurrentLapTime {
		get {
			return elapsedTime;
		}
	}

	public float BestLapTime {
		get {
			if (bestLap != -1) return lapTimes[bestLap];
			else return -1;
		}
	}

	// Use this for initialization
	void Start() {
		//Time.timeScale = 0;
		StartCoroutine(Countdown());
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Checkpoint");

		checkpoints = new EdgeCollider2D[temp.Length];
		for (int i = 0; i < temp.Length; i++) checkpoints[i] = temp[temp.Length - 1 - i].GetComponent<EdgeCollider2D>(); //HACK Inverted order of array, because I forgot how the hierarchy worked

		popupText = GameObject.Find("Popup Text").GetComponent<PopupText>();

		lapTimes = new float[totalLaps];
	}

	// Update is called once per frame
	void Update() {
		elapsedTime += Time.deltaTime;

		if (Input.GetButtonDown("Restart")) Restart();
		if (Input.GetButtonDown("Quit")) SceneManager.LoadScene("Menu");
		if (Input.GetButtonDown("Mute")) GetComponentInChildren<AudioSource>().mute = !GetComponentInChildren<AudioSource>().mute;
	}

	private void Restart() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MainScene");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Checkpoint") {
			int checkpointNum = int.Parse(collision.name.Substring(11, 2)); //HACK getting the index from the checkpoint name

			if (checkpointNum == lastVisitedCheckpoint + 1 % checkpoints.Length || checkpointNum == 0) {
				visitedCheckpoints++;
				if (visitedCheckpoints == checkpoints.Length) Lap();
			} else {
				popupText.PushToScreen("Wrong Way!", 3f);
			}

			lastVisitedCheckpoint = checkpointNum;

			//TODO some kind of wrong way notification (low priority)
		}
	}

	private void RaceEnd() {
		currentLap = totalLaps;

		if (TotalElapsedTime < Statistics._instance.bestRaceTime) Statistics._instance.bestRaceTime = TotalElapsedTime;

		popupText.PushToScreen("Race Ended!\n" +
							  $"Final Time: {TimerUI.FormatFloatTime(TotalElapsedTime)}\n" +
							  $"Best  Time: {TimerUI.FormatFloatTime(Statistics._instance.bestRaceTime)}\n" +
							  "Press R to Restart\n" +
							  "Press Esc to Quit ", 30);
		Time.timeScale = 0;
	}

	void Lap() {
		lapTimes[currentLap - 1] = elapsedTime;
		if (bestLap == -1) bestLap = currentLap - 1;
		else if (lapTimes[bestLap] > lapTimes[currentLap - 1]) {
			bestLap = currentLap - 1;
			if (lapTimes[bestLap] < Statistics._instance.bestLapTime) Statistics._instance.bestLapTime = lapTimes[bestLap];
		}

		elapsedTime = 0;

		currentLap++;
		visitedCheckpoints = 0;

		if (currentLap <= totalLaps) popupText.PushToScreen($"Lap {currentLap}", 2f);
		else RaceEnd();
	}

	IEnumerator Countdown() {
		Time.timeScale = 0;
		GetComponentInChildren<AudioSource>().Stop();
		yield return new WaitForSecondsRealtime(1);
		popupText.PushToScreen("3!", 1f);
		AudioSource.PlayClipAtPoint(countdownSound,transform.position);
		yield return new WaitForSecondsRealtime(1);
		popupText.PushToScreen("2!", 1f);
		AudioSource.PlayClipAtPoint(countdownSound, transform.position);
		yield return new WaitForSecondsRealtime(1);
		popupText.PushToScreen("1!", 1f);
		AudioSource.PlayClipAtPoint(countdownSound, transform.position);
		yield return new WaitForSecondsRealtime(1);
		popupText.PushToScreen("GO!", 1f);
		AudioSource.PlayClipAtPoint(countdownSound, transform.position);
		elapsedTime = 0;
		Time.timeScale = 1;
		yield return new WaitForSecondsRealtime(0.2f);
		GetComponentInChildren<AudioSource>().Play();
	}
}
