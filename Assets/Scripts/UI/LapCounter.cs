using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour {

	Text text;
	CarRaceLogic car;

	// Use this for initialization
	void Start() {
		text = GetComponent<Text>();

		car = GameObject.Find("Player Car").GetComponent<CarRaceLogic>();
	}

	// Update is called once per frame
	void Update() {
		text.text = "Lap " + car.currentLap + "/" + car.totalLaps;
	}
}
