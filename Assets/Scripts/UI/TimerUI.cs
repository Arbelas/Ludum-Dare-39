using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

	CarRaceLogic car;
	Text totalTime;
	Text lapTime;

	// Use this for initialization
	void Start() {
		car = GameObject.Find("Player Car").GetComponent<CarRaceLogic>();
		totalTime = GetComponent<Text>();
		lapTime = GetComponentsInChildren<Text>()[1]; //HACK to get correct timer
		//Debug.Log(lapTime.name);
	}

	// Update is called once per frame
	void LateUpdate() {
		totalTime.text = FormatFloatTime(car.TotalElapsedTime);
		lapTime.text = "Lap Time: " + FormatFloatTime(car.CurrentLapTime) +"\nBest Lap: " + (car.BestLapTime != -1 ? FormatFloatTime(car.BestLapTime) : "NA");
	}

	public static  string FormatFloatTime(float timeInSeconds) {
		int minutes = Mathf.FloorToInt(timeInSeconds / 60) % 60;
		float seconds = timeInSeconds % 60;
		return string.Format("{0:00}:{1:00.00}",minutes, seconds);
	}
}
