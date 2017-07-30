using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceLogic : MonoBehaviour {

	//TODO Add push to screen system like in LDJ 38
	//TODO Pick name and add it to player settings

	EdgeCollider2D[] checkpoints;
	PopupText popupText;
	int lastVisitedCheckpoint = 0;
	int visitedCheckpoints = -1;

	public int currentLap = 1;
	public int totalLaps = 3;

	// Use this for initialization
	void Start() {
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Checkpoint");
		//Debug.Log(checkpoints[0].name);

		checkpoints = new EdgeCollider2D[temp.Length];
		for (int i = 0; i < temp.Length; i++) checkpoints[i] = temp[temp.Length - 1 - i].GetComponent<EdgeCollider2D>(); //HACK Inverted order of array, because I forgot how the hierarchy worked

		//foreach (EdgeCollider2D e in checkpoints) Debug.Log(e.name);

		popupText = GameObject.Find("Popup Text").GetComponent<PopupText>();
		//popupText.PushToScreen("Test", 1);
	}

	// Update is called once per frame
	void Update() {

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Checkpoint") {
			int checkpointNum = int.Parse(collision.name.Substring(11, 2)); //HACK getting the index from the checkpoint name

			if (checkpointNum == lastVisitedCheckpoint + 1 % checkpoints.Length || checkpointNum == 0) {
				visitedCheckpoints++;
				if (visitedCheckpoints == checkpoints.Length) {
					currentLap++;
					visitedCheckpoints = 0;

					if(currentLap <= totalLaps) popupText.PushToScreen("Lap " + currentLap, 2f);
					else RaceEnd();

				}
			} else {
				popupText.PushToScreen("Wrong Way!", 3f);
			}

			lastVisitedCheckpoint = checkpointNum;

			//TODO some kind of wrong way notification (low priority)
		}
	}

	private void RaceEnd() {
		popupText.PushToScreen("Race Ended!\nFinal Time: <TIME>\nKeybinds for restart and quit", 30);
		currentLap = totalLaps;
	}
}
