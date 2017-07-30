using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceLogic : MonoBehaviour {

	EdgeCollider2D[] checkpoints;
	int lastVisitedCheckpoint = 0;

	public int currentLap = 0;
	public int totalLaps = 3;

	// Use this for initialization
	void Start() {
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Checkpoint");
		//Debug.Log(checkpoints[0].name);

		checkpoints = new EdgeCollider2D[temp.Length];
		for (int i = 0; i < temp.Length; i++) checkpoints[i] = temp[temp.Length - 1 - i].GetComponent<EdgeCollider2D>(); //HACK Inverted order of array, because I forgot how the hierarchy worked

		//foreach (EdgeCollider2D e in checkpoints) Debug.Log(e.name);

	}

	// Update is called once per frame
	void Update() {

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Checkpoint") {
			int checkpointNum = int.Parse(collision.name.Substring(11,2)); //HACK getting the index from the checkpoint name
			if (checkpointNum > lastVisitedCheckpoint || checkpointNum == 0) lastVisitedCheckpoint = checkpointNum;

			if (lastVisitedCheckpoint == 0) {
				currentLap++;
				Debug.Log("Beginning Lap " + currentLap);
			}

			//TODO some kind of wrong way notification (low priority)
		}
	}
}
